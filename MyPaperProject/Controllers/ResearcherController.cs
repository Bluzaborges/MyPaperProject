using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
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
            DbAreaPostgre dbArea= new DbAreaPostgre();
            DbSubareaPostgre dbSubarea= new DbSubareaPostgre();

            ViewBag.Areas = dbArea.GetAllAreas();

            return View();
        }

        [HttpPost]
        public JsonResult RegisterResearcher([FromBody] Researcher researcher)
        {
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
            bool result = false;
            int idResearcher = 0;

			try
            {
                if (string.IsNullOrEmpty(researcher.Name))
					return Json(new { success = result, message = "Nome do pesquisador não preenchido." });

				if (researcher.Name.Length > 150)
					return Json(new { success = result, message = "Nome maior que o número de caracteres permitido." });

				if (string.IsNullOrEmpty(researcher.Cpf))
					return Json(new { success = result, message = "CPF do pesquisador não preenchido." });

				if (researcher.Cpf.Length != 11)
					return Json(new { success = result, message = "CPF inválido." });

                if (dbResearcher.ResearcherExists(researcher.Name, researcher.Cpf))
					return Json(new { success = result, message = "Pesquisador já cadastrado." });

				idResearcher = dbResearcher.RegisterResearcher(researcher);

                if (idResearcher != 0)
                {
                    for (int i = 0; i < researcher.idAreas.Count; i++)
                        dbResearcher.RegisterResearcherAreas(idResearcher, researcher.idAreas[i]);

                    for (int i = 0; i < researcher.idSubareas.Count; i++)
                        dbResearcher.RegisterResearcherSubareas(idResearcher, researcher.idSubareas[i]);

                    result = true;

                } else
					return Json(new { success = result, message = "Não foi possível inserir o pesquisador." });

			} catch (Exception ex)
            {
                Log.Add(LogType.error, "[ResearcherController.RegisterResearcher]: " + ex.Message);
            }

			return Json(new { success = result, message = idResearcher });
        }

		[HttpPost]
		public JsonResult GetAllResearchers()
		{
			DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

			List<Researcher> researchers = dbResearcher.GetAllResearchers();

			return Json(researchers);
		}

		[HttpPost]
        public JsonResult GetAllResearchersAreas()
        {
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

            List<Researcher> researchers = dbResearcher.GetAllResearchersAreas();

            return Json(researchers);
        }
	}
}
