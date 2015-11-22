namespace MVC.WebUI.Controllers
{
	using System.Web.Mvc;

	public class HelpController : Controller
	{
		// GET: /help
		public ViewResult Index()
		{
			return this.View();
		}

		// GET: /help/cookies
		public ViewResult Cookies()
		{
			return this.View();
		}

		// GET: /help/enable-cookies
		[ActionName("enable-cookies")]
		public ViewResult EnableCookies()
		{
			return this.View();
		}

		// GET: /help/enable-javascript
		[ActionName("enable-javascript")]
		public ViewResult JavaScript()
		{
			return this.View();
		}

		// GET: /help/browser
		public ViewResult Browser()
		{
			return this.View();
		}
	}
}