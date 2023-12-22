using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace INTERNPRO.Controllers.Admin
{
    [Authorize(Policy = "Admin")]
    public class PCCTController : Controller
    {
        public readonly InternProjectContext _db;
        public PCCTController(InternProjectContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var lop = _db.LopHocs.Select(x => x.TenLop).ToList();
            ViewBag.Lop = lop;
            return View(ViewBag.Lop);
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
        public async Task<IActionResult> GetGVMH()
        {
            string TL = HttpContext.GetRouteValue("TenLop") as string;
            string Ca = HttpContext.GetRouteValue("Ca") as string;
            int ca = Int32.Parse(Ca);
            var listca = _db.PhanCongCts.Where(x => x.Ca == ca / 10 && x.Ngay == ca % 10).ToList();
            var mhs = _db.MonHocs.ToList();
            var pcct=_db.PhanCongCts.Where(x=>x.TenLop==TL).ToList();
            var WasMH = (from pc in pcct
                         join mh in mhs on pc.MaMh equals mh.MaMh
                        select new
                        {
                            MaMh= pc.MaMh,
                            MaGv= pc.MaGv,
                            Monhoc=mh.TenMh,
                            TenLop=pc.TenLop,
                            Ca=pc.Ca,
                            Ngay= pc.Ngay,

                        }).ToList();
            var gvmh = _db.GiaoViens.ToList();
            if (_db.PhanCongCts.Where(c => c.MaMh == 1 && c.TenLop == TL).Count() >= 4) { gvmh.RemoveAll(x => x.ChuyenMon == "Toán"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 2 && c.TenLop == TL).Count() >= 4) { gvmh.RemoveAll(x => x.ChuyenMon == "Ngữ Văn"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 3 && c.TenLop == TL).Count() >= 4) { gvmh.RemoveAll(x => x.ChuyenMon == "Tiếng Anh"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 4 && c.TenLop == TL).Count() >= 3) { gvmh.RemoveAll(x => x.ChuyenMon == "Vật Lý"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 5 && c.TenLop == TL).Count() >= 3) { gvmh.RemoveAll(x => x.ChuyenMon == "Hóa Học"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 6 && c.TenLop == TL).Count() >= 3) { gvmh.RemoveAll(x => x.ChuyenMon == "Sinh Học"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 7 && c.TenLop == TL).Count() >= 1) { gvmh.RemoveAll(x => x.ChuyenMon == "Địa Lý"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 8 && c.TenLop == TL).Count() >= 3) { gvmh.RemoveAll(x => x.ChuyenMon == "Lịch sử"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 9 && c.TenLop == TL).Count() >= 2) { gvmh.RemoveAll(x => x.ChuyenMon == "GDCD"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 10 && c.TenLop == TL).Count() >= 2) { gvmh.RemoveAll(x => x.ChuyenMon == "Tin học"); }
            if (_db.PhanCongCts.Where(c => c.MaMh == 11 && c.TenLop == TL).Count() >= 1) { gvmh.RemoveAll(x => x.ChuyenMon == "Công nghệ"); }
            var dv = gvmh;
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            if (listca.Count == 0)
            {
                if (WasMH.Count() == 0)
                {
                    string json = await Task.Run(() =>
                    {
                        return JsonConvert.SerializeObject(gvmh, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    });
                    return Json(json);
                }
                else
                {
                    string TenMH;
                    var mh = (from mhdk in WasMH
                              join Mh in mhs on mhdk.MaMh equals Mh.MaMh
                              select new MHModel
                              {
                                  MaMH = Mh.MaMh,
                                  MH = Mh.TenMh,
                                  GV = mhdk.MaGv
                              }).ToList();
                    var gv1 = gvmh.Where(item => mh.Any(x => x.MH == item.ChuyenMon && x.GV == item.MaGv))
                        .Select(item => new
                        {
                            ChuyenMon = item.ChuyenMon,
                            MaGv = item.MaGv,
                        }).ToList();
                    var gv2 = gvmh.Where(item => !mh.Any(x => x.MH == item.ChuyenMon))
                        .Select(item => new
                        {
                            ChuyenMon = item.ChuyenMon,
                            MaGv = item.MaGv,
                        }).ToList();
                    var Listgvmh = gv1.Concat(gv2).ToList();
                    string json = await Task.Run(() =>
                    {
                        return JsonConvert.SerializeObject(Listgvmh, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    });
                    return Json(json);
                }
            }
            else
            {
                var WasGV = gvmh.Where(item => listca.Any(x => x.MaGv == item.MaGv)).ToList();
                if (WasMH.Count == 0)
                {
                    var GVMH = gvmh.Where(item => !listca.Any(x => x.MaGv == item.MaGv)).ToList();
                    string json = await Task.Run(() =>
                    {
                        return JsonConvert.SerializeObject(GVMH, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    });
                    return Json(json);
                }
                else
                {

                    var mh = from g in gvmh 
                             join m in WasMH on g.MaGv equals m.MaGv
                             where !listca.Any(x => x.MaGv == g.MaGv)
                             select g;
                    var gv = gvmh.Where(item => !listca.Any(x => x.MaGv == item.MaGv) && !WasMH.Any(x=>x.MaGv != item.MaGv&&x.Monhoc==item.ChuyenMon)).ToList();
                    var Listgvmh = mh.Union(gv)
                                    .Select(item => new
                                    {
                                        ChuyenMon = item.ChuyenMon,
                                        MaGv = item.MaGv,
                                    }).ToList();
                    string json = await Task.Run(() =>
                    {
                        return JsonConvert.SerializeObject(Listgvmh, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    });
                    return Json(json);
                }
            }
        }
        [HttpPost]
        [Route("/PCCT/GetCalendars/{lop}/{ca}/{str}")]
        public IActionResult PostPCCT(string str)
        {
            string lop = HttpContext.GetRouteValue("lop") as string;
            string ca = HttpContext.GetRouteValue("ca") as string;
            if (!string.IsNullOrEmpty(str))
            {
                string[] magv = str.Split('-');
                var MH = _db.MonHocs.SingleOrDefault(x => x.TenMh == magv[1]);
                var pcct = new PhanCongCt
                {
                    MaCt = _db.PhanCongCts.Max(x => x.MaCt) + 1,
                    MaGv = Int32.Parse(magv[0]),
                    MaMh = MH.MaMh,
                    TenLop = lop,
                    Ca = int.Parse(ca) / 10,
                    Ngay = int.Parse(ca) % 10,
                };
                _db.PhanCongCts.Add(pcct);
                _db.SaveChanges();
                return Json("post thành công!");
            }
            return Json("Dữ liệu không tồn tại!");
        }
        [HttpDelete]
        [Route("/PCCT/GetCalendars/{ma}")]
        public async Task<IActionResult> DeletePCCT(int ma)
        {
            var pcct = _db.PhanCongCts.SingleOrDefault(x => x.MaCt == ma);
            _db.PhanCongCts.Remove(pcct);
            await Task.Run(() => _db.SaveChanges());
            return Json("delete thành công!");
        }
    }
}
