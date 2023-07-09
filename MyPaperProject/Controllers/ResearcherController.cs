using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
using MyPaperProject.Models;

namespace MyPaperProject.Controllers
{
    public class ResearcherController : Controller
    {
		#region Views

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

		public IActionResult Edit(int id)
		{
			DbAreaPostgre dbArea = new DbAreaPostgre();
			DbSubareaPostgre dbSubarea = new DbSubareaPostgre();
			DbResearcherPostgre dbResearcher = new DbResearcherPostgre();

			Researcher researcher = dbResearcher.GetResearcherById(id);
			researcher.Id = id;

			researcher.Areas = dbArea.GetAllAreasByIdResearcher(id);
			researcher.Subareas = dbSubarea.GetAllSubareasByIdResearcher(id);

			return View("Register", researcher);
		}

		#endregion

		#region Public Methods

		[HttpPost]
        public JsonResult RegisterResearcher([FromBody] Researcher researcher)
        {
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
			int idResearcher = 0;
			bool result = false;

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

				if (string.IsNullOrEmpty(researcher.Type))
					return Json(new { success = result, message = "Tipo do pesquisador não preenchido." });

				if (researcher.Areas.Count <= 0)
					return Json(new { success = result, message = "Áreas do pesquisador não selecionadas." });

				if (researcher.Subareas.Count <= 0)
					return Json(new { success = result, message = "Subáreas do pesquisador não selecionadas." });

				if (dbResearcher.ResearcherExists(researcher.Name, researcher.Cpf) && researcher.Id == 0)
					return Json(new { success = result, message = "Pesquisador já cadastrado." });

                if (researcher.Id == 0)
                {
					idResearcher = dbResearcher.RegisterResearcher(researcher);

					if (idResearcher != 0)
					{
						for (int i = 0; i < researcher.Areas.Count; i++)
							dbResearcher.RegisterResearcherAreas(idResearcher, researcher.Areas[i].Id);

						for (int i = 0; i < researcher.Subareas.Count; i++)
							dbResearcher.RegisterResearcherSubareas(idResearcher, researcher.Subareas[i].Id);

						result = true;
					}
					else
						return Json(new { success = result, message = "Não foi possível inserir o pesquisador." });

				} else
                    result = dbResearcher.UpdateResearcher(researcher);

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
			DbAreaPostgre dbArea = new DbAreaPostgre();
			DbSubareaPostgre dbSubarea = new DbSubareaPostgre();
			List<Researcher> researchers = new List<Researcher>();

			try
			{
				researchers = dbResearcher.GetAllResearchers();

				foreach (Researcher researcher in researchers)
				{
					researcher.Areas = dbArea.GetAllAreasByIdResearcher(researcher.Id);
					researcher.Subareas = dbSubarea.GetAllSubareasByIdResearcher(researcher.Id);
					researcher.ProjectsCount = dbResearcher.GetResearcherProjectsCount(researcher.Id);
				}
			} catch (Exception ex)
			{
				Log.Add(LogType.error, "[ResearcherController.GetAllResearchers]: " + ex.Message);
			}

			return Json(researchers);
		}

		[HttpPost]
        public JsonResult GetAllResearchersAreas()
        {
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
			List<Researcher> researchers = new List<Researcher>();

			try
			{
				researchers = dbResearcher.GetAllResearchersAreas();

			} catch (Exception ex)
			{
				Log.Add(LogType.error, "[ResearcherController.GetAllResearchersAreas]: " + ex.Message);
			}

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

		#endregion
	}
}
