namespace MVC.WebUI.Controllers
{
	using System.Web.Mvc;

	/// <summary>
	/// Error pages that fall outside the 500 Status Code range.
	/// </summary>
	internal class ErrorController : Controller
	{
		[ActionName("error-404")]
		public ViewResult NotFound()
		{
			Response.StatusCode = 404;
			return this.View();
		}

		[ActionName("error-file-upload-size")]
		public ViewResult MaxFileUploadSize()
		{
			Response.StatusCode = 404;
			Response.SubStatusCode = 13;
			return this.View();
		}

		[ActionName("error-401")]
		public ViewResult AccessDenied()
		{
			Response.StatusCode = 401;
			return this.View();
		}
	}
}