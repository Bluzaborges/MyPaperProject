using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
using MyPaperProject.Models;

namespace MyPaperProject.Controllers
{
    public class ProjectController : Controller
    {
		#region Views

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

            Project project = new Project();
            project.Id = 0;

            return View("Register", project);
        }

		public IActionResult Edit(int id)
		{
			DbAreaPostgre dbArea = new DbAreaPostgre();
			DbFundingPostgre dbFunding = new DbFundingPostgre();
			DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
			DbProjectPostgre dbProject = new DbProjectPostgre();

			ViewBag.Areas = dbArea.GetAllAreas();
			ViewBag.Fundings = dbFunding.GetAllFundings();
			ViewBag.Researchers = dbResearcher.GetAllResearchers();

			Project project = dbProject.GetProjectById(id);
			project.Id = id;

			project.Areas = dbArea.GetAllAreasByIdProject(project.Id);
			project.Researchers = dbResearcher.GetAllResearchersByIdProject(project.Id);

			return View("Register", project);
		}

		#endregion

		#region Public Methods

		[HttpPost]
        public JsonResult RegisterProject([FromBody] Project project)
        {
			DbProjectPostgre dbProject = new DbProjectPostgre();
            DbResultPostgre dbResult = new DbResultPostgre();
			bool result = false;
			int idProject = 0;

			try
			{
				if (string.IsNullOrEmpty(project.Name))
					return Json(new { success = result, message = "Nome do projeto não preenchido." });

				if (project.Name.Length > 200)
					return Json(new { success = result, message = "Nome maior que o número de caracteres permitido." });

				if (project.Areas.Count <= 0)
					return Json(new { success = result, message = "Áreas do projeto não selecionadas." });

				if (project.Researchers.Count <= 0)
					return Json(new { success = result, message = "Pesquisadores ou docentes do projeto não selecionados." });

				if (project.Funded && project.Funding.Id == 0)
					return Json(new { success = result, message = "Financiamento do projeto não selecionado." });

				if (project.Ended && project.EndedDate == DateTime.MinValue )
					return Json(new { success = result, message = "Data de término do projeto não selecionada." });

				if (project.Description.Length > 10000)
					return Json(new { success = result, message = "Descrição maior que o número de caracteres permitido." });

				if (dbProject.ProjectExists(project.Name) && project.Id == 0)
					return Json(new { success = result, message = "Projeto já cadastrado." });

                if (project.Id == 0)
                {
					idProject = dbProject.RegisterProject(project);

					if (idProject != 0)
					{
						for (int i = 0; i < project.Areas.Count; i++)
							dbProject.RegisterProjectsAreas(idProject, project.Areas[i].Id);

						for (int i = 0; i < project.Researchers.Count; i++)
							dbProject.RegisterProjectsResearchers(idProject, project.Researchers[i].Id);

						for (int i = 0; i < project.IdResults.Count; i++)
							dbProject.RegisterProjectsResults(idProject, project.IdResults[i]);

						result = true;
					}
					else
						return Json(new { success = result, message = "Não foi possível inserir o projeto." });
				} else
                {
					result = dbProject.UpdateProject(project);

                    List<int> registeredResuls = dbResult.GetAllResultsByIdProject(project.Id);

                    List<int> newResults = project.IdResults.Except(registeredResuls).ToList();

					for (int i = 0; i < newResults.Count; i++)
						dbProject.RegisterProjectsResults(project.Id, newResults[i]);
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[ProjectController.RegisterProject]: " + ex.Message);
			}

			return Json(new { success = result, message = idProject });
		}

        [HttpPost]
        public JsonResult GetAllProjects()
        {
            DbProjectPostgre dbProject = new DbProjectPostgre();
            DbResearcherPostgre dbResearcher = new DbResearcherPostgre();
            DbAreaPostgre dbArea = new DbAreaPostgre();
			List<Project> projects = new List<Project>();

			try
			{
				projects = dbProject.GetAllProjects();

				foreach (Project project in projects)
				{
					project.Areas = dbArea.GetAllAreasByIdProject(project.Id);
					project.Researchers = dbResearcher.GetAllResearchersByIdProject(project.Id);
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[ProjectController.GetAllProjects]: " + ex.Message);
			}

			return Json(projects);
        }

		#endregion
	}
}
