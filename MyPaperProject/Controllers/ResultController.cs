using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
using MyPaperProject.Models;

namespace MyPaperProject.Controllers
{
	public class ResultController : Controller
	{
		#region Views

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult RenderResultSection([FromBody] int idResult)
		{
			DbResultPostgre dbResult = new DbResultPostgre();

			Result result = dbResult.GetResultById(idResult);

			result.Id = idResult;
			result.Attachments = dbResult.GetAttachmentsByIdResult(idResult);

			return PartialView("_ResultSectionPartial", result);
		}

		public ActionResult DownloadAttachment(int idAttachment)
		{
			DbResultPostgre dbResult = new DbResultPostgre();

			Attachment attachment = dbResult.GetAttachmentById(idAttachment);

			if (attachment.Content != null)
				return File(attachment.Content, System.Net.Mime.MediaTypeNames.Application.Octet, attachment.Name);

			return NotFound();
		}

		#endregion

		#region Public Methods

		[HttpPost]
		public JsonResult RegisterResult([FromBody] Result result)
		{
			DbResultPostgre dbResult = new DbResultPostgre();
			int idResult = 0;
			bool response = false;

			try
			{
				if (string.IsNullOrEmpty(result.Name))
					return Json(new { success = response, message = "Nome do resultado não preenchido." });

				if (result.Name.Length > 200)
					return Json(new { success = response, message = "Nome maior que o número de caracteres permitido." });

				if (result.Description.Length > 5000)
					return Json(new { success = response, message = "Descrição maior que o número de caracteres permitido." });

				idResult = dbResult.RegisterResult(result);

				if (idResult == 0)
					return Json(new { success = response, message = "Não foi possível inserir o resultado." });

				foreach (Attachment attachment in result.Attachments)
				{
					if (!string.IsNullOrEmpty(attachment.Name))
					{
						string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\temp-files\\" + attachment.Name);

						if (System.IO.File.Exists(filePath))
						{
							MemoryStream memoryStream = new MemoryStream();
							FileStream fileStream = new FileStream(filePath, FileMode.Open);

							fileStream.CopyTo(memoryStream);

							attachment.Content = memoryStream.ToArray();

							fileStream.Close();
							memoryStream.Close();

							System.IO.File.Delete(filePath);
						}

						response = dbResult.RegisterAttachment(idResult, attachment);

						if (!response)
							return Json(new { success = response, message = "Não foi possível inserir o anexo." });
					}
				}

				response = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[ResultController.RegisterResult]: " + ex.Message);
			}

			return Json(new { success = response, message = idResult });
		}

		[HttpPost]
		public JsonResult GetAllResultsByIdProject([FromBody] int idProject)
		{
			DbResultPostgre dbResult = new DbResultPostgre();
			List<int> results = new List<int>();

			try
			{
				results = dbResult.GetAllResultsByIdProject(idProject);

			} catch (Exception ex)
			{
				Log.Add(LogType.error, "[ResultController.GetAllResultsByIdProject]: " + ex.Message);
			}

			return Json(results);
		}

		[HttpPost]
		public JsonResult UploadFile(IFormFile file)
		{
			string uploadFilesPath = Config.uploadFilesPath;

			if (!Directory.Exists(uploadFilesPath))
				Directory.CreateDirectory(uploadFilesPath);

			string filePath = Path.Combine(uploadFilesPath, file.FileName);

			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);

			FileStream fileStream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fileStream);
			fileStream.Close();

			FileInfo info = new FileInfo(filePath);

			var fileInfo = new
			{
				size = info.Length,
				name = info.Name,
				extension = info.Extension,
				url = info.Name,
			};

			return new JsonResult(fileInfo);
		}

		#endregion
	}
}
