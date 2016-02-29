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
                View = "~/views/shared/errors/httprequestvalidationexception.cshtml",
                Order = 1
            });

            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(HttpAntiForgeryException),
                View = "~/views/shared/errors/httpantiforgeryexception.cshtml",
                Order = 1
            });

            // custom error attribute to log all errors
            filters.Add(new GlobalErrorAttribute());

            // enables the default error page
            filters.Add(new HandleErrorAttribute
            {
                View = "~/views/shared/errors/error.cshtml"
            });
        }
    }
}