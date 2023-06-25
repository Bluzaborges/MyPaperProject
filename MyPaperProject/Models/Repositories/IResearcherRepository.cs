namespace MyPaperProject.Models.Repositories
{
	public interface IResearcherRepository
	{
		bool ResearcherExists(string name, string cpf);
		int RegisterResearcher(Researcher researcher);
		bool RegisterResearcherAreas(int idResearcher, int idArea);
		bool RegisterResearcherSubareas(int idResearcher, int idSubarea);

		public List<Researcher> GetAllResearchers();
	}
}
