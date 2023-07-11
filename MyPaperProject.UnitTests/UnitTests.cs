namespace MyPaperProject.UnitTests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void RegisterResearcher_sendEmptyName()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = string.Empty;

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_checkNameExceedsCharacters()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = new string(Enumerable.Repeat('a', 151).ToArray());

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_sendEmptyCpf()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = "Test";
			researcher.Cpf = string.Empty;

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_checkCpfExceedsCharacters()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = "Test";
			researcher.Cpf = new string(Enumerable.Repeat('a', 12).ToArray());

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_sendEmptyType()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = "Test";
			researcher.Cpf = "00000000000";

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_sendEmptyAreasList()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = "Test";
			researcher.Cpf = "00000000000";
			researcher.Type = ResearcherType.Student.ToString();
			researcher.Areas = new List<Area>();

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResearcher_sendEmptySubareasList()
		{
			ResearcherController researcherController = new ResearcherController();

			Researcher researcher = new Researcher();
			researcher.Name = "Test";
			researcher.Cpf = "00000000000";
			researcher.Type = ResearcherType.Student.ToString();
			researcher.Subareas = new List<Subarea>();

			Area area = new Area();
			area.Id = 0;

			researcher.Areas = new List<Area>();
			researcher.Areas.Add(area);

			var result = researcherController.RegisterResearcher(researcher);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_sendEmptyName()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = string.Empty;

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_checkNameExceedsCharacters()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = new string(Enumerable.Repeat('a', 201).ToArray());

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_sendEmptyAreasList()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = "Test";
			project.Areas = new List<Area>();

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_sendEmptyResearcherList()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = "Test";
			project.Areas = new List<Area>();
			project.Researchers = new List<Researcher>();

			Area area = new Area();
			area.Id = 1;

			project.Areas.Add(area);

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_sendEmptyFunding()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = "Test";
			project.Areas = new List<Area>();
			project.Researchers = new List<Researcher>();

			Area area = new Area();
			area.Id = 1;

			project.Areas.Add(area);

			project.Funded = true;
			project.Funding.Id = 0;

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterProject_sendEmptyEndedDate()
		{
			ProjectController projectController = new ProjectController();

			Project project = new Project();
			project.Name = "Test";
			project.Areas = new List<Area>();
			project.Researchers = new List<Researcher>();

			Area area = new Area();
			area.Id = 1;

			project.Areas.Add(area);

			project.Funded = true;
			project.Funding.Id = 1;

			project.Ended = true;
			project.EndedDate = DateTime.MinValue;

			var result = projectController.RegisterProject(project);
			var data = result.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResult_sendEmptyName()
		{
			ResultController resultController = new ResultController();

			Result result = new Result();
			result.Name = string.Empty;

			var response = resultController.RegisterResult(result);
			var data = response.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}

		[Test]
		public void RegisterResult_checkNameExceedsCharacters()
		{
			ResultController resultController = new ResultController();

			Result result = new Result();
			result.Name = new string(Enumerable.Repeat('a', 201).ToArray());

			var response = resultController.RegisterResult(result);
			var data = response.Value;

			bool success = (bool)data.GetType().GetProperty("success").GetValue(data, null);

			Assert.IsFalse(success);
		}
	}
}