using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.GV
{
    public class TKBGVController : Controller
    {
        private readonly InternProjectContext _db;
        public TKBGVController(InternProjectContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/TKBGV/{MaGV}")]
        public IActionResult Index()
        {
            string magv = HttpContext.GetRouteValue("MaGV") as string;
            int Ma = int.Parse(magv);
            if (magv != null)
            {
                var tkb = _db.PhanCongCts.Where(s => s.MaGv == Ma).Select(x => new CTGV
                {
                    TenLop = x.TenLop,
                    Ca = x.Ca,
                    Ngay = x.Ngay,
                });
                return View(tkb);
            }
            else { return Json("lỗi"); }
        }
    }
}
