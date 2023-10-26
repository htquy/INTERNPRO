using INTERNPRO.Datas;
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
            var gv=_db.GiaoViens.ToList();
            var LuongGv = from L in luong
                          join G in gv on L.MaLuong equals G.MaLuong
                          select new
                          {
                              MaGV=G.MaGv,
                              TenGV=G.TenGv,
                              MaL=G.MaLuong,
                              Luong=L.MucLuongCb
                          };
            return View(LuongGv);
        }
    }
}
