using INTERNPRO.Datas;
using INTERNPRO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.HS
{
    public class DiemHSController : Controller
    {

        private readonly InternProjectContext _db;
        public DiemHSController(InternProjectContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/DiemHS/{MaHS}")]
        public IActionResult Index()
        {
            var mahs = int.Parse(HttpContext.GetRouteValue("MaHS") as string);
            var dsdiem = _db.Diems.Where(x => x.MaHs.Equals(mahs)).ToList();
            var mh = _db.MonHocs.ToList();
            var listdiem = (from d in dsdiem
                            join m in mh on d.MaMh equals m.MaMh
                            select new DSDiemHSModel
                            {
                                MaMh = d.MaMh,
                                TenMh = m.TenMh,
                                Lop = d.Lop,
                                MaHs = d.MaHs,
                                Diem15 = d.Diem15,
                                DiemCk = d.DiemCk,
                                DiemGk = d.DiemGk,
                                Ki = d.Ki,
                            }).ToList();
            ViewBag.diems = listdiem;
            ViewBag.ma = mahs;
            return View();
        }
    }
}
