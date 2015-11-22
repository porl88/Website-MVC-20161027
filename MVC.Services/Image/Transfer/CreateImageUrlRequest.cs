namespace MVC.Services.Images.Transfer
{
	public class CreateImageUrlRequest
	{
		public string DirectoryPath { get; set; }

		public string ImageName { get; set; }

		public string MimeType { get; set; }
	}
}
