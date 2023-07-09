namespace MyPaperProject.Models.Repositories
{
	public interface IAreaRepository
	{
		List<Area> GetAllAreas();
		List<Area> GetAllAreasByIdResearcher(int idResearcher);
		List<Area> GetAllAreasByIdProject(int idProject);

    }
}
