using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace INTERNPRO.Controllers.Admin
{
    namespace INTERNPRO.Controllers
    {
        public class GiaoVienController : Controller
        {
            private readonly InternProjectContext _db;
            private readonly IWebHostEnvironment _en;
            public GiaoVienController(InternProjectContext db, IWebHostEnvironment en)
            {
                _db = db;
                _en = en;
            }

            public IActionResult Index()
            {
                return View();
            }
            [HttpGet]
            public IActionResult GetGV()
            {

                List<GiaoVien> giaoviens = new List<GiaoVien>();
                giaoviens = _db.GiaoViens.ToList();
                int maxID;
                if (giaoviens.Count == 0)
                {
                    maxID = DateTime.Now.Year * 100;
                }
                else if (giaoviens.Max(s => s.MaGv) < DateTime.Now.Year * 100)
                {
                    maxID = DateTime.Now.Year * 100;
                }
                else
                {
                    maxID = giaoviens.Max(s => s.MaGv);
                }
                ViewBag.GV = giaoviens;
                ViewBag.magv = maxID;
                return View();
            }
            [HttpGet]
            [Route("GiaoVien/GetGV/{MaGV}")]
            public IActionResult GetDataileGV(int MaGV)
            {
                var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv == MaGV);
                return Json(gv);
            }
            [HttpPost]
            public async Task<IActionResult> InsertGV(GiaoVienModel gv)
            {

                if (ModelState.IsValid)
                {
                    string wwwRootPath = _en.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(gv.ImageFile.FileName);
                    string extension = Path.GetExtension(gv.ImageFile.FileName);
                    fileName = fileName + gv.MaGv.ToString() + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await gv.ImageFile.CopyToAsync(fileStream);
                    }
                    var Teacher = new GiaoVien()
                    {
                        MaGv = gv.MaGv,
                        PassWord = gv.PassWord,
                        QueQuan = gv.QueQuan,
                        TenGv = gv.HoTenGv,
                        ChuyenMon = gv.ChuyenMon,
                        GioiTinh = gv.GioiTinh,
                        MaLuong = gv.MaLuong,
                        NgayBatDau = gv.NgayBatDau,
                        NgaySinh = gv.NgaySinh,
                        Motakhac = gv.MoTaKhac,
                        ChuNhiemLop = gv.ChuNhiemLop,
                        Anh = fileName
                    };

                    _db.GiaoViens.Add(Teacher);
                    _db.SaveChanges();
                    return Json("Them thanh cong");
                }
                return Json("Lỗi!!!");

            }
            [HttpDelete]
            [Route("GiaoVien/RemoveGV/{magv}")]
            public IActionResult RemoveGV(int magv)
            {
                var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv == magv);
                if (gv != null)
                {
                    //System.IO.File.Delete("wwwroot\\Image" + gv.Anh);
                    _db.GiaoViens.Remove(gv);
                    _db.SaveChanges();
                    return Json("Xoa thanh cong!");
                }
                return Json("Doi tuong khong co trong du lieu");
            }
            [HttpPost]
            [Route("GiaoVien/PutGV")]
            public async Task<IActionResult> PutGV(GiaoVienModel gv)
            {
                if (ModelState.IsValid)
                {
                    var giaovien = _db.GiaoViens.SingleOrDefault(x => x.MaGv == gv.MaGv);
                    if (giaovien != null)
                    {
                        if (gv.ImageFile != null)
                        {
                            string wwwRootPath = _en.WebRootPath;
                            string fileName = Path.GetFileNameWithoutExtension(gv.ImageFile.FileName);
                            string extension = Path.GetExtension(gv.ImageFile.FileName);
                            fileName = fileName + gv.MaGv.ToString() + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await gv.ImageFile.CopyToAsync(fileStream);
                            }
                            giaovien.Anh = fileName;
                        }
                        giaovien.MaGv = gv.MaGv;
                        giaovien.PassWord = gv.PassWord;
                        giaovien.QueQuan = gv.QueQuan;
                        giaovien.TenGv = gv.HoTenGv;
                        giaovien.ChuyenMon = gv.ChuyenMon;
                        giaovien.GioiTinh = gv.GioiTinh;
                        giaovien.MaLuong = gv.MaLuong;
                        giaovien.NgaySinh = gv.NgaySinh;
                        giaovien.NgayBatDau = gv.NgayBatDau;
                        giaovien.Motakhac = gv.MoTaKhac;
                        giaovien.ChuNhiemLop = gv.ChuNhiemLop;
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
}