using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;

namespace MyPaperProject.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
			DbAreaPostgre dbArea = new DbAreaPostgre();
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

			ViewBag.Areas = dbArea.GetAllAreas();
            ViewBag.Researchers = dbResearcher.GetAllResearchers();

			return View();
        }
    }
}
