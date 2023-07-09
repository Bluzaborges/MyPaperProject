namespace MyPaperProject.Models.Repositories
{
	public interface IResearcherRepository
	{
		Researcher GetResearcherById(int id);
		List<Researcher> GetAllResearchers();
		List<Researcher> GetAllResearchersAreas();
		List<Researcher> GetAllResearchersByIdProject(int idProject);
		int GetResearcherProjectsCount(int idResearcher);
		int RegisterResearcher(Researcher researcher);
		bool RegisterResearcherAreas(int idResearcher, int idArea);
		bool RegisterResearcherSubareas(int idResearcher, int idSubarea);
		bool UpdateResearcher(Researcher researcher);
		bool DeleteResearcherById(int id);
		bool ResearcherExists(string name, string cpf);
		bool ResearcherHaveProject(int idResearcher);
	}
}
