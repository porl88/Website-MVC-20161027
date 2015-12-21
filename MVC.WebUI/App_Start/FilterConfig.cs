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
			 * NB:  CustomErrors must be ON in web.config for these to work.
             *      Only applies to 500 errors.
             *      Needs to be registered in the Global.asax file: FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters).
			 */

            // http://community.codesmithtools.com/CodeSmith_Community/b/tdupont/archive/2011/03/01/error-handling-and-customerrors-and-mvc3-oh-my.aspx
            filters.Add(new HandleErrorAttribute
			{
				ExceptionType = typeof(HttpRequestValidationException),
				View = "error-request-validation",
                Order = 1
			});

			filters.Add(new HandleErrorAttribute
			{
				ExceptionType = typeof(HttpAntiForgeryException),
				View = "error-cookies-disabled",
                Order = 1
			});

			filters.Add(new GlobalErrorAttribute());

            filters.Add(new HandleErrorAttribute());
		}
	}
}