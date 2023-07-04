namespace MyPaperProject.Models
{
	public class Project
	{
		public int Id { get; set; }
		public List<int> IdAreas { get; set; }
		public List<string> AreasNames { get; set; }
		public List<int> IdResearchers { get; set; }
		public List<string> ResearchersNames { get; set; }
		public List<int> IdTeachers { get; set; }
		public List<string> TeachersNames { get; set; }
		public List<int> IdResults { get; set; }
		public int IdFunding { get; set; }
		public string FundingName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Funded { get; set; }
		public bool Ended { get; set; }
		public DateTime EndedDate { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
