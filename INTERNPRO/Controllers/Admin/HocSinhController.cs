using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text.RegularExpressions;

namespace INTERNPRO.Controllers.Admin
{
    public class HocSinhController : Controller
    {
        private readonly InternProjectContext _db;
        private readonly IWebHostEnvironment _en;
        public HocSinhController(InternProjectContext db, IWebHostEnvironment en)
        {
            _db = db;
            _en = en;
        }

        public IActionResult Index()
        {
            var lop = _db.LopHocs.Select(x => x.TenLop).ToList();
            ViewBag.Lop = lop;
            return View(ViewBag.Lop);
        }
        [HttpGet("GetHS")]
        [Route("/HocSinh/GetHS/{Lop}")]
        public IActionResult GetHS(string Lop)
        {
            List<HocSinh> hocsinhs = new List<HocSinh>();
            hocsinhs = _db.HocSinhs.Where(x => x.TenLop == Lop).OrderBy(s => s.HoTenHs).ToList();
            var dshs = _db.HocSinhs.ToList();
            int maxID;
            if (dshs.Count == 0)
            {
                maxID = DateTime.Now.Year * 10000;
            }
            else if (dshs.Max(s => s.MaHs) < DateTime.Now.Year * 10000)
            {
                maxID = DateTime.Now.Year * 10000;
            }
            else
            {
                maxID = dshs.Max(s => s.MaHs);
            }
            ViewBag.HS = hocsinhs;
            ViewBag.mahs = maxID;
            return View();
        }
        [HttpGet]
        [Route("HocSinh/GetDetailHS/{MaHS}")]
        public IActionResult GetDataileHS(int MaHS)
        {
            var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == MaHS);
            return Json(hs);
        }
        [HttpPost]
        public async Task<IActionResult> InsertHS(HocSinhModel hs)
        {

            if (ModelState.IsValid)
            {
                string phoneNumberParttern = @"0\d{9}$";
                if (!Regex.IsMatch(hs.SoDienThoaiHs, phoneNumberParttern))
                {
                    return Json("Số Điện Thoại Không Hợp Lệ");
                }
                string fileName;
                if (hs.ImageFile != null)
                {
                    string wwwRootPath = _en.WebRootPath;
                    fileName = Path.GetFileNameWithoutExtension(hs.ImageFile.FileName);
                    string extension = Path.GetExtension(hs.ImageFile.FileName);
                    fileName = fileName + hs.MaHs.ToString() + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await hs.ImageFile.CopyToAsync(fileStream);
                    }
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
                        HoTenPh = hs.HoTenPh,
                        NgayNh = hs.NgayNh,
                        Anh = fileName
                    };
                    _db.HocSinhs.Add(Student);
                    _db.SaveChanges();
                }
                else
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
                        NgayNh = hs.NgayNh,
                        HoTenPh = hs.HoTenPh,
                    };
                    _db.HocSinhs.Add(Student);
                    _db.SaveChanges();
                }
                return Json("Them thanh cong");
            }
            return Json("Lỗi!!!");

        }
        [HttpDelete]
        [Route("HocSinh/RemoveHS/{mahs}")]
        public IActionResult RemoveHS(int mahs)
        {
            var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == mahs);
            var diem = _db.Diems.Where(x => x.MaHs == mahs);
            if (hs != null)
            {
                if (diem != null)
                {

                    _db.Diems.RemoveRange(diem);
                    _db.SaveChanges();
                }
                _db.HocSinhs.Remove(hs);
                _db.SaveChanges();
                return Json("Xoa thanh cong!");
            }
            return Json("Doi tuong khong co trong du lieu");
        }
        [HttpPost]
        [Route("HocSinh/PutHS")]
        public async Task<IActionResult> PutHS(HocSinhModel hs)
        {
            if (ModelState.IsValid)
            {
                var hocsinh = _db.HocSinhs.SingleOrDefault(x => x.MaHs == hs.MaHs);
                if (hocsinh != null)
                {
                    if (hs.ImageFile != null)
                    {
                        string wwwRootPath = _en.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(hs.ImageFile.FileName);
                        string extension = Path.GetExtension(hs.ImageFile.FileName);
                        fileName = fileName + hs.MaHs.ToString() + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await hs.ImageFile.CopyToAsync(fileStream);
                        }
                        hocsinh.Anh = fileName;
                    }
                    hocsinh.MaHs = hs.MaHs;
                    hocsinh.PassWord = hs.PassWord;
                    hocsinh.SoDienThoaiPh = hs.SoDienThoaiPh;
                    hocsinh.SoDienThoaiHs = hs.SoDienThoaiHs;
                    hocsinh.QueQuan = hs.QueQuan;
                    hocsinh.HoTenPh = hs.HoTenPh;
                    hocsinh.NgaySinh = hs.NgaySinh;
                    hocsinh.HoTenHs = hs.HoTenHs;
                    hocsinh.HoTenPh = hs.HoTenPh;
                    hocsinh.GioiTinh = hs.GioiTinh;
                    hocsinh.TenLop = hs.TenLop;
                    hocsinh.NgayNh = hs.NgayNh;
                    _db.SaveChanges();
                    return Json("Sua doi thanh cong");
                }
                return Json("Sua doi khong hop le");
            }
            return Json("Lỗi!!!");
        }
        [HttpGet]
        public IActionResult Log()
        {

            var redirectUrl = "/";
            return Json(new { redirectUrl });

        }
    }
}