namespace MyPaperProject.Models
{
    public class Researcher
    {
		public int Id { get; set; }
		public string Name { get; set; }
        public string Cpf { get; set; }
        public string Type { get; set; }
        public List<Area> Areas { get; set; }
        public List<Subarea> Subareas { get; set; }
		public int IdArea { get; set; }
        public int ProjectsCount { get; set; }
	}

    public enum ResearcherType
    {
        Student,
        Teacher,
        Employee
    }
}
