namespace MyPaperProject.Models.Repositories
{
	public interface ISubareaRepository
	{
		List<Subarea> GetAllSubareas();
		List<Subarea> GetAllSubareasByIdResearcher(int idResearcher);
    }
}
