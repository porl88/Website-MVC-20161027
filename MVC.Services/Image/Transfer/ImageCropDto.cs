namespace MVC.Services.Images.Transfer
{
	public class ImageCropDto
    {
		public int X { get; set; }

		public int Y { get; set; }

		public int TargetWidth { get; set; }

		public int TargetHeight { get; set; }

		public int SelectedWidth { get; set; }

		public int SelectedHeight { get; set; }
	}
}
