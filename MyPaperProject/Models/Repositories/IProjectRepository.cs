namespace MyPaperProject.Models.Repositories
{
	public interface IProjectRepository
	{
		public Project GetProjectById(int id);
		public List<Project> GetAllProjects();
		public int RegisterProject(Project project);
		public bool RegisterProjectsAreas(int idProject, int idArea);
		public bool RegisterProjectsResearchers(int idProject, int idResearcher);
		public bool RegisterProjectsResults(int idProject, int idResult);
		public bool UpdateProject(Project project);
		public bool ProjectExists(string name);
	}
}
