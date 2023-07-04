using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Global;
using MyPaperProject.Models;
using System.IO;

namespace MyPaperProject.Controllers
{
	public class ResultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public JsonResult RegisterResult([FromBody] Result result)
		{
			DbResultPostgre dbResult = new DbResultPostgre();
			bool response = false;

			if (string.IsNullOrEmpty(result.Name))
				return Json(new { success = response, message = "Nome do resultado não preenchido." });

			if (result.Name.Length > 200)
				return Json(new { success = response, message = "Nome maior que o número de caracteres permitido." });

			int idResult = dbResult.RegisterResult(result);

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

			return Json(new { success = response, message = idResult });
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
	}
}
