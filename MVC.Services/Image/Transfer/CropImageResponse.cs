namespace MVC.Services.Images.Transfer
{
	using System.Drawing;

	public class CropImageResponse : BaseResponse
	{
		public Image CroppedImage { get; set; }
	}
}
