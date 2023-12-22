using INTERNPRO.Datas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers.GV
{
    [Authorize(Policy = "GV")]
    public class TTGVController : Controller
    {
        private readonly InternProjectContext _db;
        public TTGVController(InternProjectContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/TTGV/{MaGv}")]
        public IActionResult Index()
        {
            var magv = int.Parse(HttpContext.GetRouteValue("MaGv") as string);
            var gv = _db.GiaoViens.SingleOrDefault(x => x.MaGv==magv);
            ViewBag.GV = gv;
            return View(ViewBag.GV);
        }
    }
}
