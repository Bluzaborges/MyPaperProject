using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Models;

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
            DbFundingPostgre dbFunding = new DbFundingPostgre();
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

            ViewBag.Areas = dbArea.GetAllAreas();
            ViewBag.Fundings = dbFunding.GetAllFundings();
            ViewBag.Researchers = dbResearcher.GetAllResearchers();

            return View();
        }

        [HttpPost]
        public JsonResult RegisterProject([FromBody] Project project)
        {
            return Json(project);
        }
    }
}
