using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public JsonResult RegisterResearcher([FromBody] Researcher researcher)
        {
            return Json(researcher);
        }
    }
}
