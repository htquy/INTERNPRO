using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.HS
{
    [Authorize(Policy = "HS")]
    public class TKBHSController : Controller
    {
        private readonly InternProjectContext _db;
        public TKBHSController(InternProjectContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/TKBHS/{MaHs}")]
        public IActionResult Index()
        {
            string mahs = HttpContext.GetRouteValue("MaHs") as string;
            string lop = _db.HocSinhs.SingleOrDefault(x => x.MaHs == int.Parse(mahs)).TenLop;
            if (lop == null)
            {
                return Ok();
            }
            else
            {
                var pcs = _db.PhanCongCts.Where(x => x.TenLop == lop).ToList();
                var gvs = _db.GiaoViens.ToList();
                var mhs = _db.MonHocs.ToList();
                var tkb = (from gv in gvs
                           join pc in pcs on gv.MaGv equals pc.MaGv
                           join mh in mhs on pc.MaMh equals mh.MaMh
                           select new TKBHsModel
                           {
                               TenGv = gv.TenGv,
                               Mh = mh.TenMh,
                               Ca = pc.Ca,
                               Ngay = pc.Ngay,
                           }
                          ).ToList();
                ViewBag.Tkb = tkb;
                ViewBag.Lop = lop;
                ViewBag.Ma = int.Parse(mahs);
                return View(tkb);
            }
        }
    }
}
