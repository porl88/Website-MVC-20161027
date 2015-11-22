namespace ClassLibrary.Images
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ImageEditor
    {
        private readonly Image image;

        public ImageEditor(Image image)
        {
            this.image = image;
        }

        public Image CropImage(Rectangle cropArea)
        {
            using (var bmpImage = new Bitmap(this.image))
            {
                return bmpImage.Clone(cropArea, bmpImage.PixelFormat); // cannot use a 'using' statement on the returned image
            }
        }

        public Image ResizeImage(int width)
        {
            double scale = (double)width / (double)this.image.Width;
            int height = (int)Math.Round(this.image.Height * scale, 0);
            var size = new Size(width, height);
            return this.ResizeImage(this.image, size);
        }

        private Image ResizeImage(Image image, Size size)
        {
            var resizedImg = new Bitmap(size.Width, size.Height); // cannot use a 'using' statement on the returned image
            using (var graphic = Graphics.FromImage(resizedImg))
            {
                resizedImg.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                // image quality settings
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // create new image
                graphic.DrawImage(image, 0, 0, size.Width, size.Height);

                return resizedImg;
            }
        }

        private double GetAspectRatio(int width, int height)
        {
            return Math.Round((double)(width / height), 2);
        }
    }
}



/*
 * 
 * public class ImageEditorService : IImageEditorService
	{
		public bool ValidateImage(Stream fileStream, string fileType, double maxFileSize, Size minSize, params string[] permittedImageTypes)
		{
			var megaByte = Math.Pow(2, 20);

			if (fileType.StartsWith("image/") && permittedImageTypes.Contains(fileType) && fileStream.Length <= (maxFileSize * megaByte))
			{
				using (var image = new Bitmap(fileStream))
				{
					if (image.Width >= minSize.Width && image.Height >= minSize.Height)
					{
						return true;
					}
				}
			}

			return false;
		}

		public Image CropImage(Image image, Rectangle cropArea)
		{
			using (var bmpImage = new Bitmap(image))
			{
				return bmpImage.Clone(cropArea, bmpImage.PixelFormat); // cannot use a 'using' statement on the returned image
			}
		}

        public Image CropImage(Image image, ImageCrop crop)
        {
            if (image.Width >= crop.TargetWidth && image.Height >= crop.TargetHeight)
            {
                var selectedAspectRatio = this.GetAspectRatio(crop.SelectedWidth, crop.SelectedHeight);
                var targetAspectRatio = this.GetAspectRatio(crop.TargetWidth, crop.TargetHeight);

                if (image.Width == crop.TargetWidth && image.Height == crop.TargetHeight)
                {
                    // if the image is the same size as the target crop:
                    return image;
                }
                else if (selectedAspectRatio == targetAspectRatio)
                {
                    // if the selected crop has the same aspect ratio as the target crop:
                    if (image.Width == crop.SelectedWidth && image.Height == crop.SelectedHeight)
                    {
                        // if the selected crop is the same size as the original image, no crop is needed:
                        return this.ResizeImage(image, crop.TargetWidth);
                    }
                    else
                    {
                        // if the selected crop is smaller than the original image:
                        var cropArea = new Rectangle(crop.X, crop.Y, crop.SelectedWidth, crop.SelectedHeight);
                        using (var croppedImage = this.CropImage(image, cropArea))
                        {
                            if (croppedImage.Width == crop.TargetWidth && croppedImage.Height == crop.TargetHeight)
                            {
                                return (Image)croppedImage.Clone();
                            }
                            else
                            {
                                return this.ResizeImage(croppedImage, crop.TargetWidth);
                            }
                        }
                    }
                }
            }

            return null;
        }

		public Image ResizeImage(Image image, int width)
		{
			double scale = (double)width / (double)image.Width;
			int height = (int)Math.Round(image.Height * scale, 0);
			var size = new Size(width, height);
			return this.ResizeImage(image, size);
		}

		private Image ResizeImage(Image image, Size size)
		{
			var resizedImg = new Bitmap(size.Width, size.Height); // cannot use a 'using' statement on the returned image
			using (var graphic = Graphics.FromImage(resizedImg))
			{
				resizedImg.SetResolution(image.HorizontalResolution, image.VerticalResolution);

				// image quality settings
				graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphic.SmoothingMode = SmoothingMode.HighQuality;
				graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphic.CompositingQuality = CompositingQuality.HighQuality;
				graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

				// create new image
				graphic.DrawImage(image, 0, 0, size.Width, size.Height);

				return resizedImg;
			}
		}

        private double GetAspectRatio(int width, int height)
        {
            return Math.Round((double)(width / height), 2);
        }

        // NOT YET USED:
        private void EnsureDirectoryStructureExists(string filePath)
        {
            var filePathInfo = new FileInfo(filePath);
            filePathInfo.Directory.Create();
        }
	}
 */