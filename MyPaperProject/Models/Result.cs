namespace MyPaperProject.Models
{
	public class Result
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public List<Attachment> Attachments { get; set; }
	}
}
