using Microsoft.AspNetCore.Mvc;

namespace MyPaperProject.Controllers
{
	public class ResultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}
	}
}
