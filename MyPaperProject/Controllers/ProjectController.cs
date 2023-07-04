using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
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
			DbProjectPostgre dbProject = new DbProjectPostgre();
			bool result = false;
			int idProject = 0;

			project.IdResearchers = project.IdResearchers.Concat(project.IdTeachers).ToList();

			try
			{
				if (string.IsNullOrEmpty(project.Name))
					return Json(new { success = result, message = "Nome do projeto não preenchido." });

				if (project.Name.Length > 200)
					return Json(new { success = result, message = "Nome maior que o número de caracteres permitido." });

				if (dbProject.ProjectExists(project.Name))
					return Json(new { success = result, message = "Projeto já cadastrado." });

				idProject = dbProject.RegisterProject(project);

				if (idProject != 0)
				{
                    for (int i = 0; i < project.IdAreas.Count; i++)
                        dbProject.RegisterProjectsAreas(idProject, project.IdAreas[i]);

					for (int i = 0; i < project.IdResearchers.Count; i++)
						dbProject.RegisterProjectsResearchers(idProject, project.IdResearchers[i]);

					for (int i = 0; i < project.IdResults.Count; i++)
						dbProject.RegisterProjectsResults(idProject, project.IdResults[i]);

					result = true;

				} else
					return Json(new { success = result, message = "Não foi possível inserir o projeto." });
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
            DbFundingPostgre dbFunding = new DbFundingPostgre();
            DbAreaPostgre dbArea = new DbAreaPostgre();

            List<Project> projects = dbProject.GetAllProjects();

            foreach (Project project in projects)
            {
                List<Researcher> researchers = dbResearcher.GetAllResearchersByIdProject(project.Id);

                project.ResearchersNames = researchers.Where(r => r.Type == ResearcherType.Student.ToString() || r.Type == ResearcherType.Employee.ToString()).Select(r => r.Name).ToList();
                project.TeachersNames = researchers.Where(r => r.Type == ResearcherType.Teacher.ToString()).Select(r => r.Name).ToList();

                project.AreasNames = dbArea.GetAllAreasNamesByIdProject(project.Id);

                project.FundingName = dbFunding.GetFundingById(project.IdFunding);
            }

            return Json(projects);
        }

    }
}
