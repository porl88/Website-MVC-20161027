namespace MVC.Services.Images
{
	using Services.Images.Transfer;

    public interface IImageService
    {
		string CreateImageUrl(CreateImageUrlRequest request);

        CropImageResponse CropImage(ImageCropDto crop);
	}
}
