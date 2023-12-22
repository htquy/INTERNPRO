using INTERNPRO.Datas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.HS
{
    [Authorize(Policy = "HS")]
    public class TTHSController : Controller
    {
        private readonly InternProjectContext _db;
        public TTHSController(InternProjectContext db)
        {
            _db = db;
        }
        [Authorize(Policy = "HS")]
        [HttpGet]
        [Route("/TTHS/{MaHS}")]
        public IActionResult Index()
        {
            var mahs = int.Parse(HttpContext.GetRouteValue("MaHS") as string);
            var hs = _db.HocSinhs.SingleOrDefault(x => x.MaHs == mahs);
            ViewBag.Hs = hs;
            return View(ViewBag.Hs);
        }
    }
}
