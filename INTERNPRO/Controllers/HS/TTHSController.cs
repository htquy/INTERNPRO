using INTERNPRO.Datas;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.HS
{
    public class TTHSController : Controller
    {
        private readonly InternProjectContext _db;
        public TTHSController(InternProjectContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/TTHS/{MaHS}")]
        public IActionResult Index()
        {
            var mahs=int.Parse(HttpContext.GetRouteValue("MaHS")as string);
            var hs = _db.HocSinhs.SingleOrDefault(x=>x.MaHs==mahs);
            return View(hs);
        }
    }
}
