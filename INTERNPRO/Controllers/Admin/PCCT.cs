using INTERNPRO.Datas;
using INTERNPRO.Models;
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
            string TL= HttpContext.GetRouteValue("TenLop") as string;
            string Ca= HttpContext.GetRouteValue("Ca") as string;
            int ca=Int32.Parse(Ca);
            var listca=_db.PhanCongCts.Where(x=>x.Ca==ca/10&&x.Ngay==ca%10).ToList();
            var WasMH = _db.PhanCongCts.Where(x => x.TenLop == TL).ToList();
            var mhs = _db.MonHocs.ToList();
            var gvmh = _db.GiaoViens.ToList();
            if (listca.Count == 0)
            {
                if (WasMH.Count == 0)
                {
                    var GVMH = _db.GiaoViens.ToList();
                    return Json(GVMH);
                }
                else
                {
                    string TenMH;
                    var mh = (from mhdk in WasMH
                             join Mh in mhs on mhdk.MaMh equals Mh.MaMh
                             select new MHModel
                             {
                                 MaMH=Mh.MaMh,
                                 MH=Mh.TenMh,
                                 GV=mhdk.MaGv
                             }).ToList();
                    var gv1 = gvmh.Where(item => mh.Any(x => x.MH == item.ChuyenMon&&x.GV==item.MaGv))
                        .Select(item=>new
                        {
                            chuyenMon=item.ChuyenMon,
                            maGv=item.MaGv,
                        }).ToList();
                    var gv2 = gvmh.Where(item => !mh.Any(x => x.MH == item.ChuyenMon))
                        .Select(item => new
                        {
                            chuyenMon = item.ChuyenMon,
                            maGv = item.MaGv,
                        }).ToList();

                    var Listgvmh = gv1.Concat(gv2).ToList();
                    return Json(Listgvmh);
                }
            }
            else
            {
                var WasGV = gvmh.Where(item => listca.Any(x => x.MaGv == item.MaGv)).ToList();
                if (WasMH.Count == 0)
                {
                    var GVMH = WasGV.Where(item => !listca.Any(x => x.MaGv == item.MaGv)).ToList();
                    return Json(GVMH);
                }
                else
                {
                    var mh = from mhdk in WasMH
                             join Mh in mhs on mhdk.MaMh equals Mh.MaMh
                             select new
                             {
                                 mhdk.MaMh,
                                 Mh.TenMh,
                                 mhdk.MaGv,
                             };
                    var gv = gvmh.Where(item => !listca.Any(x => x.MaGv == item.MaGv) && mh.Any(x => x.TenMh == item.ChuyenMon&&x.MaGv==item.MaGv)).ToList();
                    var Listgvmh = WasGV.Union(gv)
                                    .Select(item => new
                                    {
                                        chuyenMon = item.ChuyenMon,
                                        maGv = item.MaGv,
                                    }).ToList();
                    return Json(Listgvmh);
                }
            }
        }
        [HttpPost]
        [Route("/PCCT/GetCalendars/{lop}/{ca}/{str}")]
        public IActionResult PostPCCT(string str)
        {
            string lop = HttpContext.GetRouteValue("lop") as string;
            string ca= HttpContext.GetRouteValue("ca") as string;
            if (!string.IsNullOrEmpty(str))
            {
                string[] magv=str.Split('-');
                var MH = _db.MonHocs.SingleOrDefault(x => x.TenMh == magv[1]);
                var pcct = new PhanCongCt
                {
                    MaCt=_db.PhanCongCts.Max(x => x.MaCt)+1,
                    MaGv = Int32.Parse(magv[0]),
                    MaMh = MH.MaMh,
                    TenLop=lop,
                    Ca=int.Parse(ca)/10,
                    Ngay=int.Parse(ca)%10,
                };
                _db.PhanCongCts.Add(pcct);
                _db.SaveChanges();
                return Json(pcct);
            }return View();
        }
        [HttpDelete]
        [Route("/PCCT/GetCalendars/{ma}")]
        public IActionResult DeletePCCT(int ma)
        {
            var pcct=_db.PhanCongCts.SingleOrDefault(x=> x.MaCt==ma);
            _db.PhanCongCts.Remove(pcct);
            _db.SaveChanges();
            return Json(pcct);
        }
    }
}
