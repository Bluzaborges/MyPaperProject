namespace MyPaperProject.Models
{
    public class Researcher
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Type { get; set; }
        public List<int> idAreas { get; set; }
        public List<int> idSubareas { get; set; }

    }
}
