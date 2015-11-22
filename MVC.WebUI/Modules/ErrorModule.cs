namespace MVC.WebUI.Modules
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using WebUI.Controllers;

	// NB: Register in web.config System.WebServer/Modules
	public class ErrorModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.EndRequest += new EventHandler(this.Application_EndRequest);
		}

		public void Dispose()
		{
		}

		// http://www.codeproject.com/Articles/635324/Another-set-of-ASP-NET-MVC-4-tips
		protected void Application_EndRequest(object sender, EventArgs e)
		{
			var context = HttpContext.Current;

			if (context != null)
			{
				switch (context.Response.StatusCode)
				{
					//Not authorized
					case 401:
						this.RedirectToErrorPage("error-401", context);
						break;

					//Not found
					case 404:

						if (context.Response.SubStatusCode == 13)
						{
							// Max file size limit - set in web.config system.web/httpRuntime/@maxLengthRequest and system.webServer/security/requestFiltering/requestLimits/@maxAllowedContentLength
							this.RedirectToErrorPage("error-file-upload-size", context);
						}
						else
						{
							this.RedirectToErrorPage("error-404", context);
						}
						
						break;
				}
			}
		}

		private void RedirectToErrorPage(string action, HttpContext context)
		{
			if (!string.IsNullOrWhiteSpace(action))
			{
				context.Response.Clear();

				var wrapper = new HttpContextWrapper(context);
				var controller = new ErrorController() as IController;
				var route = new RouteData();

				route.Values["controller"] = "Error";
				route.Values["action"] = action;

				controller.Execute(new RequestContext(wrapper, route));
			}
		}
	}
}