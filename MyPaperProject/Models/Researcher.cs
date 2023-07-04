namespace MyPaperProject.Models
{
    public class Researcher
    {
		public int Id { get; set; }
        public int IdArea { get; set; }
		public string Name { get; set; }
        public string Cpf { get; set; }
        public string Type { get; set; }
        public List<int> idAreas { get; set; }
        public List<int> idSubareas { get; set; }
        public List<string> nameAreas { get; set; }
        public List<string> nameSubareas { get; set; }
    }

    public enum ResearcherType
    {
        Student,
        Teacher,
        Employee
    }
}
