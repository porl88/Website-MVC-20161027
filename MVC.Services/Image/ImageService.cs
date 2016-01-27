namespace MVC.Services.Images
{
    using System;
    using System.Drawing;
    using ClassLibrary.Extensions.Image;
    using Core.Exceptions;
    using Services.Images.Transfer;

    public class ImageService : IImageService
	{
		private readonly Image image;

		public ImageService(Image image)
		{
			this.image = image;
		}

		public string CreateImageUrl(CreateImageUrlRequest request)
		{
			throw new NotImplementedException();
		}

		public CropImageResponse CropImage(ImageCropDto crop)
		{
			var response = new CropImageResponse();

			if (image.Width >= crop.TargetWidth && image.Height >= crop.TargetHeight)
			{
				var selectedAspectRatio = this.GetAspectRatio(crop.SelectedWidth, crop.SelectedHeight);
				var targetAspectRatio = this.GetAspectRatio(crop.TargetWidth, crop.TargetHeight);

				if (image.Width == crop.TargetWidth && image.Height == crop.TargetHeight)
				{
					// if the image is the same size as the target crop:
					response.Status = StatusCode.OK;
					response.CroppedImage = this.image;
				}
				else if (selectedAspectRatio == targetAspectRatio)
				{
					// if the selected crop has the same aspect ratio as the target crop:
					if (image.Width == crop.SelectedWidth && image.Height == crop.SelectedHeight)
					{
						// if the selected crop is the same size as the original image, no crop is needed:
						response.Status = StatusCode.OK;
						response.CroppedImage = this.image.Resize(crop.TargetWidth);
					}
					else
					{
						// if the selected crop is smaller than the original image:
						var cropArea = new Rectangle(crop.X, crop.Y, crop.SelectedWidth, crop.SelectedHeight);
						using (var croppedImage = this.image.Crop(cropArea))
						{
							if (croppedImage.Width == crop.TargetWidth && croppedImage.Height == crop.TargetHeight)
							{
								response.Status = StatusCode.OK;
								response.CroppedImage = (Image)croppedImage.Clone();
							}
							else
							{
								response.Status = StatusCode.OK;
								response.CroppedImage = croppedImage.Resize(crop.TargetWidth);
							}
						}
					}
				}
			}
			else
			{
				response.Status = StatusCode.BadRequest;
			}

			return response;
		}

		private double GetAspectRatio(int width, int height)
		{
			return Math.Round((double)(width / height), 2);
		}
	}
}
