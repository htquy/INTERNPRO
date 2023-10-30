using INTERNPRO.Datas;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers
{
    public class AccountController : Controller
    {
        private readonly InternProjectContext _db;
        private readonly IWebHostEnvironment _en;
        public AccountController(InternProjectContext db, IWebHostEnvironment en)
        {
            _db = db;
            _en = en;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAccount(string code, string password)
        {
            int codeVal;
            if (int.TryParse(code, out codeVal))
            {
                var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == codeVal && x.PassWord == password);

                if (hs != null)
                {
                    var MaHS = int.Parse(code);
                    var redirectUrl = "/TTHS/" + code;
                    return Json(new { redirectUrl });
                }
                else
                {
                    var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv == codeVal && x.PassWord == password);

                    if (gv != null && gv.TenGv == "Admin")
                    {
                        var redirectUrl = "/Account/Admin";
                        return Json(new { redirectUrl });
                    }
                    else if (gv != null)
                    {
                        var MaGv = int.Parse(code);
                        var redirectUrl = "/TTGV/"+MaGv;
                        return Json(new { redirectUrl });
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
       
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
