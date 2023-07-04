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

            Project project = new Project();

            project.Id = 0;

            return View("Register", project);
        }

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

				if (dbProject.ProjectExists(project.Name) && project.Id == 0)
					return Json(new { success = result, message = "Projeto já cadastrado." });

                if (project.Id == 0)
                {
					project.IdResearchers = project.IdResearchers.Concat(project.IdTeachers).ToList();

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
            project.IdAreas = dbArea.GetAllAreasByIdProject(project.Id).Select(a => a.Id).ToList();

			List<Researcher> researchers = dbResearcher.GetAllResearchersByIdProject(project.Id);

			project.IdResearchers = researchers.Where(r => r.Type == ResearcherType.Student.ToString() || r.Type == ResearcherType.Employee.ToString()).Select(r => r.Id).ToList();
			project.IdTeachers = researchers.Where(r => r.Type == ResearcherType.Teacher.ToString()).Select(r => r.Id).ToList();

			return View("Register", project);
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

                project.AreasNames = dbArea.GetAllAreasByIdProject(project.Id).Select(a => a.Name).ToList();

                project.FundingName = dbFunding.GetFundingById(project.IdFunding);
            }

            return Json(projects);
        }
	}
}
