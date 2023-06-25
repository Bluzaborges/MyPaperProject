namespace MyPaperProject.Models
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Funded { get; set; }
		public bool Ended { get; set; }
		public DateOnly EndedDate { get; set; }
		public List<int> idAreas { get; set; }
		public List<int> idResearchers { get; set; }
	}
}
