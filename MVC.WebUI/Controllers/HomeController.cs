namespace MVC.WebUI.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Core.Data.EntityFramework;
    using Core.Entities.Article;
    using Core.Entities.Website;
    using Models;
    using Services.Page;

    public class HomeController : Controller
    {
        private readonly IPageService pageService;

        public HomeController(IPageService pageService)
        {
            this.pageService = pageService;
        }

        // GET: /
        public async Task<ViewResult> Index()
        {
            //var languageCode = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
            var homePage = await this.pageService.GetPageAsync(1);

            var model = new HomeIndexViewModel
            {
                Page = homePage.Page
            };

            return View(model);
        }
    }
}