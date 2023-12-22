using INTERNPRO.Datas;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace INTERNPRO.Controllers
{
    public class AccountController : Controller
    {
        private readonly InternProjectContext _db;
        private readonly IWebHostEnvironment _en;
        private readonly IConfiguration _config;
        public AccountController(InternProjectContext db, IWebHostEnvironment en, IConfiguration config)
        {
            _db = db;
            _en = en;
            _config = config;
        }
        private String GenerateJWT( int ms)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var signatureKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,ms.ToString()),
                (ms <300000) ?((ms==1111)? new Claim(ClaimTypes.Role, "Admin"):new Claim(ClaimTypes.Role,"GV")) : new Claim(ClaimTypes.Role, "HS"),
            };
            var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(2),
                    claims: claims,
                    signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logout()
        {

            // Xóa cookie
            Response.Cookies.Delete("YourAppCookieName");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GetAccount(string code, string password)
        {
            int codeVal;
            if (int.TryParse(code, out codeVal))
            {
                var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == codeVal && x.PassWord == password);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,codeVal.ToString()),
                    (codeVal <300000) ?((codeVal==1111)? new Claim(ClaimTypes.Role, "Admin"):new Claim(ClaimTypes.Role,"GV")) : new Claim(ClaimTypes.Role, "HS"),
                };

                // Create a ClaimsIdentity and specify the authentication scheme
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Configure authentication properties
                var properties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                };

                // Sign in the user using the specified authentication scheme
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                if (hs != null)
                {
                    var MaHS = int.Parse(code);
                    var redirectUrl = "/TTHS/" + code;
                    return Json(new { url = redirectUrl, token = GenerateJWT(codeVal) });
                }
                else
                {
                    var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv == codeVal && x.PassWord == password);

                    if (gv != null && gv.TenGv == "Admin")
                    {
                        var redirectUrl = "/Account/Admin";
                        return Json(new { url = redirectUrl, token = GenerateJWT(codeVal) });
                    }
                    else if (gv != null)
                    {
                        var MaGv = int.Parse(code);
                        var redirectUrl = "/TTGV/" + MaGv;
                        return Json(new { url = redirectUrl, token = GenerateJWT(codeVal) });
                    }
                    else return Ok();
                }
            }
            else return Ok();
        }

        [HttpGet]
        [Route("/Account/GetHS/{MaHS}")]
        public IActionResult GetHS(int MaHS)
        {
            return View(MaHS);
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
