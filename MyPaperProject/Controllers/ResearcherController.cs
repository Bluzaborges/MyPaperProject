using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Models;

namespace MyPaperProject.Controllers
{
    public class ResearcherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            DbArea dbArea= new DbArea();
            DbSubarea dbSubarea= new DbSubarea();

            ViewBag.Areas = dbArea.GetAllAreas();
            ViewBag.Subareas = dbSubarea.GetAllSubareas();

            return View();
        }

        [HttpPost]
        public JsonResult RegisterResearcher([FromBody] Researcher researcher)
        {
            return Json(researcher);
        }

        [HttpPost]
		public JsonResult GetAllSubareas()
		{

			DbSubarea dbSubarea = new DbSubarea();

            List<Subarea> subareas = dbSubarea.GetAllSubareas();

			return Json(subareas);
		}
	}
}
