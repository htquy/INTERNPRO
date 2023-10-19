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
        [HttpGet]
        public IActionResult GetAccount(string code, string password)
        {
            var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == int.Parse(code) && x.PassWord == password);
            if (hs != null)
            {
                var MaHS = int.Parse(code);
                return Redirect("/Account/GetHS/"+MaHS);
            }
            else
            {
                var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv == int.Parse(code) && x.PassWord == password);
                if (gv != null)
                {
                    var MaGv = int.Parse(code);
                    return Redirect("/Account/GetGV/" + MaGv);
                }
                else return Json("Không tìm thấy dữ kiện");
            }
        }
        [HttpGet]
        [Route("/Account/GetHS/{MaHS}")]
        public IActionResult GetHS(int MaHS)
        {
            return View(MaHS);
        }
        [HttpGet]
        [Route("/Account/GetHS/{MaGv}")]
        public IActionResult GetGV(int MaGv)
        {
            return View(MaGv);
        }
    }
}
