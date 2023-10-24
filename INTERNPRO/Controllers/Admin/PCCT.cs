using INTERNPRO.Datas;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.Admin
{
    public class PCCT : Controller
    {
        public readonly InternProjectContext _db;
        public PCCT(InternProjectContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/PCCT/GetCalendars/{Tenlop}")]
        public IActionResult GetCalendars()
        {
            string tenLop = HttpContext.GetRouteValue("Tenlop") as string;
            List<PhanCongCt> listpc = new List<PhanCongCt>();
            listpc = _db.PhanCongCts.Where(x => x.TenLop == tenLop).ToList();
            var listGVMH = (from item in _db.GiaoViens
                            select new
                            {
                                GV = item.MaGv,
                                TenGV = item.TenGv,
                                CM = item.ChuyenMon
                            }).ToList();
            ViewBag.GVMH = listGVMH;
            ViewBag.TL = tenLop;
            return View(listpc);
        }
        [HttpGet]
        [Route("/PCCT/GetCalendars/{TenLop}/{Ca}")]
        public IActionResult GetGVMH() {
            string Ca= HttpContext.GetRouteValue("Ca") as string;
            int ca=Int32.Parse(Ca);
            var listca=_db.PhanCongCts.Where(x=>x.Ca==ca/10&&x.Ngay==ca%10).ToList();
            if (listca.Count == 0)
            {
                var GVMH = _db.GiaoViens.ToList();
                return Json(GVMH);
            }
            else
            {
                var GVMH = _db.GiaoViens.Where(item => !listca.Any(x => x.MaGv == item.MaGv)).ToList();
                return Json(GVMH);
            }
        }
    }
}
