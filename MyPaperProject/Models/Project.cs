namespace MyPaperProject.Models
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Area> Areas { get; set; }
		public List<Researcher> Researchers { get; set; }
		public List<int> IdResults { get; set; }
		public bool Funded { get; set; }
		public Funding Funding { get; set; }
		public bool Ended { get; set; }
		public DateTime EndedDate { get; set; }
		public DateTime CreationDate { get; set; }

		public Project()
		{
			Funding = new Funding();
		}
	}
}
