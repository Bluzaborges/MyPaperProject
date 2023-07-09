namespace MyPaperProject.Models.Repositories
{
	public interface IResultRepository
	{
		Result GetResultById(int idResult);
		Attachment GetAttachmentById(int idAttachment);
		List<Attachment> GetAttachmentsByIdResult(int idResult);
		List<int> GetAllResultsByIdProject(int idProject);
		int RegisterResult(Result result);
		bool RegisterAttachment(int idResult, Attachment attachment);
	}
}
