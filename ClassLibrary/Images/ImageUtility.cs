namespace ClassLibrary.Images
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Linq;

    public enum Rotation
    {
        Rotate90CW,
        Rotate90CCW,
        Rotate180
    }

    public static class ImageUtility
    {
		public static string GetMimeType(Image image)
		{
			var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(x => x.FormatID == image.RawFormat.Guid);
			return codec != null ? codec.MimeType : string.Empty;
		}

        public static Image Resize(this Image image, int width)
        {
            var imageEditor = new ImageEditor(image);
            return imageEditor.ResizeImage(width);
        }

        public static Image Crop(this Image image, Rectangle cropArea)
        {
            var imageEditor = new ImageEditor(image);
            return imageEditor.CropImage(cropArea);
        }

        public static Image Rotate(this Image image, Rotation rotation)
        {
            throw new NotImplementedException();
        }
    }
}
