using INTERNPRO.Datas;
using Microsoft.AspNetCore.Mvc;

namespace INTERNPRO.Controllers
{
    public class HocSinhController : Controller
    {   private readonly InternProjectContext _db;
        public HocSinhController(InternProjectContext db) 
        { _db = db; }
        public IActionResult Index()
        {
            return View(_db.HocSinhs);
        }
    }
}
