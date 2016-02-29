namespace MVC.WebUI.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    /// <summary>
    /// Error pages that fall outside the 500 Status Code range.
    /// </summary>
    [Obsolete("Use <httpErrors> section in web.config file instead.")]
    internal class ErrorController : Controller
	{
		[ActionName("error-404")]
		public ViewResult NotFound()
		{
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
			return this.View();
		}

		[ActionName("error-file-upload-size")]
		public ViewResult MaxFileUploadSize()
		{
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
			Response.SubStatusCode = 13;
			return this.View();
		}

		[ActionName("error-401")]
		public ViewResult AccessDenied()
		{
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			return this.View();
		}
	}
}