namespace MVC.WebUI
{
	using System.Web;
	using System.Web.Mvc;
	using MVC.WebUI.Attributes;

	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			/*
			 * NB: CustomErrors must be ON in web.config for these to work
			 */

			// http://community.codesmithtools.com/CodeSmith_Community/b/tdupont/archive/2011/03/01/error-handling-and-customerrors-and-mvc3-oh-my.aspx
			filters.Add(new HandleErrorAttribute
			{
				ExceptionType = typeof(HttpRequestValidationException),
				View = "error-request-validation"
			});

			filters.Add(new HandleErrorAttribute
			{
				ExceptionType = typeof(HttpAntiForgeryException),
				View = "error-cookies-disabled"
			});

			filters.Add(new GlobalErrorAttribute());
			//filters.Add(new HandleErrorAttribute());
		}
	}
}