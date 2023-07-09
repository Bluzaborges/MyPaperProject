namespace MyPaperProject.Models
{
	public class Attachment
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Content { get; set; }

		public string Extension
		{
			get
			{
				if (string.IsNullOrEmpty(Name))
					return string.Empty;
				else
					return System.IO.Path.GetExtension(Name).ToLower();
			}
		}

		public string Icon
		{
			get
			{
				string iconUrl = "/assets/media/custom_img/attachment_file.png";

				if (Extension == ".zip" || this.Extension == ".rar")
				{
					iconUrl = "/assets/media/custom_img/attachment_zip.png";
				}
				else if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png")
				{
					iconUrl = "/assets/media/custom_img/attachment_jpg.png";
				}
				else if (Extension == ".xls" || Extension == ".xlsx" || Extension == ".csv")
				{
					iconUrl = "/assets/media/custom_img/attachment_xls.png";
				}
				else if (Extension == ".txt" || Extension == ".doc" || Extension == ".docx")
				{
					iconUrl = "/assets/media/custom_img/attachment_txt.png";
				}
				else if (Extension == ".pdf")
				{
					iconUrl = "/assets/media/custom_img//attachment_pdf.png";
				}

				return iconUrl;
			}
		}
	}
}
