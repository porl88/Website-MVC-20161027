namespace MVC.WebUI
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Core.Testing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            this.SetViewEngines();
            this.Optimise();

            Database.SetInitializer(new DatabaseInitializer()); // Recreates database with test data. A call needs to be made to the database before this will run. N.B. This can alternatively be configured in the configuration/entityFramework/contexts section of the web.config file
            //GlobalConfiguration.Configure(WebApiConfig.Register); // enables Web Api - position is important - needs to come before RegisterRoutes
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }

        private void SetViewEngines()
        {
            // Remove unused view engines - http://www.codeproject.com/Articles/635324/Another-set-of-ASP-NET-MVC-4-tips#tip-17
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private void Optimise()
        {
            // Disable MVC response header - http://www.codeproject.com/Articles/635324/Another-set-of-ASP-NET-MVC-4-tips#tip-17
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
