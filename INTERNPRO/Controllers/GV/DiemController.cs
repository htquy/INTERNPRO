using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;


namespace INTERNPRO.Controllers.GV
{
    public class DiemController : Controller
    {
        private readonly InternProjectContext _db;
        private readonly IWebHostEnvironment _en;
        public DiemController(InternProjectContext db, IWebHostEnvironment en)
        {
            _db = db;
            _en = en;
        }
        [HttpGet]
        [Route("/Diem/{MaGv}")]
        public IActionResult Index()
        {
            string magv=HttpContext.GetRouteValue("MaGv") as string;
            List<string> lop = _db.PhanCongCts.Where(x=>x.MaGv==int.Parse(magv)).Select(x=>x.TenLop).ToList();
            int mamh = _db.PhanCongCts.Where(x => x.MaGv == int.Parse(magv)).Select(x => x.MaMh).FirstOrDefault();
            ViewBag.Lop = lop.Distinct();
            ViewBag.Mh = mamh;
            ViewBag.ma=magv;
            return View();
        }
        [HttpGet("GetDiem")]
        [Route("Diem/GetDiem/{lop}/{MaMh}")]
        public async Task<IActionResult> GetDiem(string Lop,int MaMh)
        {
            int ki = 0;
            ViewBag.ma =MaMh;
            ViewBag.l = Lop;
            ViewBag.magv=_db.PhanCongCts.FirstOrDefault(x=>x.MaMh==MaMh).MaGv;
            DateTime currentTime = DateTime.Now;
            int month = currentTime.Month;
            if (month < 7) ki = 2;
            else ki = 1;
            var dshs = _db.HocSinhs.Where(x => x.TenLop == Lop).ToList();
            var dsdiem=_db.Diems.Where(x=>x.Lop==Lop&&x.Ki==ki&&x.MaMh==MaMh).ToList();
            var diemlop = from ds in dshs
                          join diem in dsdiem on ds.MaHs equals diem.MaHs 
                           select new DiemModel
                           {
                               MaHs = ds.MaHs,
                               HoTenHs = ds.HoTenHs,
                               Lop = Lop,
                               MaMh=MaMh,
                               Diem15=diem.Diem15,
                               DiemCk= diem.DiemCk,
                               DiemGk= diem.DiemGk,
                               Ki=ki,
                          };
            var nohs = dshs.Where(item1 => !dsdiem.Any(item2 => item2.MaHs == item1.MaHs)).ToList();
            var nodiem = from hs in nohs
                         select new DiemModel
                         {
                             MaHs = hs.MaHs,
                             HoTenHs = hs.HoTenHs,
                             Lop = hs.TenLop,
                             Ki = ki,
                             MaMh=MaMh
                         };
            var list= diemlop.Concat(nodiem).ToList();
            return View(list);
        }
        [HttpPost]
        [Route("/Diem/PostDiem/{lop}/{ma}")]
        public IActionResult PostDiem(List<DiemModel> listdiem)
        {
            string lop = HttpContext.GetRouteValue("lop") as string;
            int mamh=int.Parse(HttpContext.GetRouteValue("ma") as string);
            var dsdiem = _db.Diems.Where(x=>x.Lop.Contains(lop)&&x.MaMh==mamh).ToList();
            foreach(var l in listdiem)
            {
                var d= dsdiem.SingleOrDefault(x=>x.MaHs==l.MaHs&&x.Ki==l.Ki);
                if(d!=null)
                {
                    d.DiemCk= l.DiemCk;
                    d.DiemGk= l.DiemGk;
                    d.Diem15= l.Diem15;
                    _db.SaveChanges();
                }
                else
                {
                    var diem = new Diem()
                    {
                        MaDiem = l.MaHs.ToString() + l.MaMh.ToString() + l.Ki.ToString() + lop,
                        MaHs= l.MaHs,
                        MaMh= mamh,
                        Diem15= l.Diem15,
                        DiemCk= l.DiemCk,
                        DiemGk= l.DiemGk,
                        Lop= lop,
                        Ki= l.Ki,
                    };
                    _db.Diems.Add(diem);
                    _db.SaveChanges();
                }
            }
            return Json("Sửa thành công");
        }
    }
}
