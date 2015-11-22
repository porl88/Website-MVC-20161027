namespace MVC.WebUI.Modules
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	// NB: Register in web.config System.WebServer/Modules
	public class CheckBrowserModule : IHttpModule
	{
		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(this.Application_BeginRequest);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			HttpContext context = HttpContext.Current;

			if (!this.BrowserIsSupported(context.Request.UserAgent))
			{
				this.RedirectToHelpPage(context);
			}
		}

		private bool BrowserIsSupported(string userAgent)
		{
			//var client = new ClientInfoService(userAgent);
			//var browser = client.Browser;
			//int version = client.BrowserMajorVersion;
			//switch (browser)
			//{
			//	case Browser.Firefox:

			//		if (version < 24)
			//		{
			//			return false;
			//		}

			//		break;
			//	case Browser.Chrome:

			//		if (version < 24)
			//		{
			//			return false;
			//		}

			//		break;
			//	case Browser.IE:

			//		if (version < 9)
			//		{
			//			return false;
			//		}

			//		break;
			//	case Browser.Safari:

			//		if (client.DeviceType == DeviceType.Desktop && version < 4)
			//		{
			//			return false;
			//		}

			//		break;
			//	case Browser.Opera:

			//		if (version < 15)
			//		{
			//			return false;
			//		}

			//		break;
			//}

			return true;
		}

		private void RedirectToHelpPage(HttpContext context)
		{
			context.Response.Clear();

			var wrapper = new HttpContextWrapper(context);
			var controller = new MVC.WebUI.Controllers.HelpController() as IController;
			var route = new RouteData();

			route.Values["action"] = "Browser";

			controller.Execute(new RequestContext(wrapper, route));
		}
	}
}