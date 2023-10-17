using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers
{
    public class HocSinhController : Controller
    {
        private readonly InternProjectContext _db;
        public HocSinhController(InternProjectContext db)
        { _db = db; }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetHS()
        {
            List<HocSinh> hocsinhs = new List<HocSinh>();
            hocsinhs = _db.HocSinhs.ToList();
            ViewBag.HS = hocsinhs;
            return View();
        }
        [HttpPost]
        public IActionResult InsertHS(HocSinhModel hs)
        {

            if (ModelState.IsValid)
            {
                var Student = new HocSinh()
                {
                    MaHs = hs.MaHs,
                    PassWord = hs.PassWord,
                    HoTenHs = hs.HoTenHs,
                    QueQuan = hs.QueQuan,
                    TenLop = hs.TenLop,
                    GioiTinh = hs.GioiTinh,
                    SoDienThoaiHs = hs.SoDienThoaiHs,
                    SoDienThoaiPh = hs.SoDienThoaiPh,
                    NgaySinh = hs.NgaySinh,
                    HoTenPh = hs.HoTenPh
                };

                _db.HocSinhs.Add(Student);
                _db.SaveChanges();
                 return RedirectToAction("Index");
             }
                    return Json("Lỗi!!!");

        }
    }
}