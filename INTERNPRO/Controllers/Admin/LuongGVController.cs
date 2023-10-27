using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.Admin
{
    public class LuongGVController : Controller
    {
        private readonly InternProjectContext _db;
        public LuongGVController(InternProjectContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetLuong() {
            var luong=_db.Luongs.ToList();
            var pcct =_db.PhanCongCts.ToList();
            if (pcct.Count != 0)
            {
                
                var gv = _db.GiaoViens.ToList();
                var LuongGv = from L in luong
                              join G in gv on L.MaLuong equals G.MaLuong
                              orderby G.TenGv
                              select new LuongModel
                              {
                                  MaGV = G.MaGv,
                                  TenGV = G.TenGv,
                                  MaL = G.MaLuong,
                                  SoCa= pcct.Where(x=>x.MaGv.Equals(G.MaGv)).Count(),
                                  Luong = L.MucLuongCb,
                                  Thuong = G.ThuongThem,
                              }
                              ;
                return View(LuongGv);
            }
            else return View();
        }
        [HttpPost] 
        public IActionResult PostLuong(List<int> dsthuong) {
            var gv = _db.GiaoViens.OrderBy(x=>x.TenGv).ToList();
            for(int i = 0; i < gv.Count(); i++)
            {
                gv[i].ThuongThem = dsthuong[i];
            }
            _db.SaveChanges();
            return Json("post thành công");
        }
        
    }
}
