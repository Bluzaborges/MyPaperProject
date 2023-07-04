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

            ViewBag.Areas = dbArea.GetAllAreas();

            Researcher researcher = new Researcher();

            researcher.Id = 0;

            return View("Register", researcher);
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

                if (dbResearcher.ResearcherExists(researcher.Name, researcher.Cpf) && researcher.Id == 0)
					return Json(new { success = result, message = "Pesquisador já cadastrado." });

                if (researcher.Id == 0)
                {
					idResearcher = dbResearcher.RegisterResearcher(researcher);

					if (idResearcher != 0)
					{
						for (int i = 0; i < researcher.idAreas.Count; i++)
							dbResearcher.RegisterResearcherAreas(idResearcher, researcher.idAreas[i]);

						for (int i = 0; i < researcher.idSubareas.Count; i++)
							dbResearcher.RegisterResearcherSubareas(idResearcher, researcher.idSubareas[i]);

						result = true;

					}
					else
						return Json(new { success = result, message = "Não foi possível inserir o pesquisador." });
				} else
                {
                    result = dbResearcher.UpdateResearcher(researcher);
                }

			} catch (Exception ex)
            {
                Log.Add(LogType.error, "[ResearcherController.RegisterResearcher]: " + ex.Message);
            }

			return Json(new { success = result, message = idResearcher });
        }

        public IActionResult Edit(int id)
        {
			DbAreaPostgre dbArea = new DbAreaPostgre();
			DbSubareaPostgre dbSubarea = new DbSubareaPostgre();
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

			ViewBag.Areas = dbArea.GetAllAreas();
            ViewBag.Subareas = dbSubarea.GetAllSubareas();

            Researcher researcher = dbResearcher.GetResearcherById(id);
            researcher.Id = id;

            researcher.idAreas = dbArea.GetAllAreasByIdResearcher(id).Select(a => a.Id).ToList();
            researcher.idSubareas = dbSubarea.GetAllSubareasByIdResearcher(id).Select(s => s.Id).ToList();

			return View("Register", researcher);
		}

		[HttpPost]
		public JsonResult GetAllResearchers()
		{
			DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
            DbAreaPostgre dbArea = new DbAreaPostgre();
            DbSubareaPostgre dbSubarea = new DbSubareaPostgre();

			List<Researcher> researchers = dbResearcher.GetAllResearchers();

            foreach (Researcher researcher in researchers)
            {
                researcher.nameAreas = dbArea.GetAllAreasByIdResearcher(researcher.Id).Select(a => a.Name).ToList();
                researcher.nameSubareas = dbSubarea.GetAllSubareasByIdResearcher(researcher.Id).Select(s => s.Name).ToList();
            }

			return Json(researchers);
		}

		[HttpPost]
        public JsonResult GetAllResearchersAreas()
        {
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

            List<Researcher> researchers = dbResearcher.GetAllResearchersAreas();

            return Json(researchers);
        }

		[HttpPost]
		public JsonResult DeleteResearcher([FromBody] int id)
		{
			DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
			var result = false;

			try
			{
				if (id == 0)
					return Json(new { success = result, message = "Não foi possível deletar o pesquisador." });

				if (dbResearcher.ResearcherHaveProject(id))
					return Json(new { success = result, message = "O pesquisador possui vínculo com 1 ou mais projetos." });

				result = dbResearcher.DeleteResearcherById(id);
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[ResearcherController.DeleteResearcher]: " + ex.Message);
			}

			return Json(new { success = result, message = "" });
		}
	}
}
