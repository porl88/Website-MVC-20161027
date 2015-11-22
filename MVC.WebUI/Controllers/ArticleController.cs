//namespace MVC.WebUI.Controllers
//{
//	using System.Net;
//	using System.Web.Mvc;
//	using MVC.Services;
//	using MVC.Services.Article;
//	using MVC.Services.Article.Transfer;
//	using MVC.WebUI.Models.Article;

//	public class ArticleController : Controller
//    {
//		private readonly IArticleService articleService;

//		public ArticleController(IArticleService articleService)
//		{
//			this.articleService = articleService;
//		}

//		// GET: /article
//		public ViewResult Index()
//		{
//			var articles = this.articleService.GetArticleSummaries();
//			return this.View(articles);
//		}

//        // GET: /article/details
//        public ActionResult Details(int? id)
//        {
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}

//			var response = this.articleService.GetArticle((int)id);

//			if (response.Status == ResponseStatus.OK)
//			{
//				return this.View(response.Article);
//			}
//			else if (response.Status == ResponseStatus.NotFound)
//			{
//				return this.HttpNotFound();
//			}
//			else
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
//			}
//        }

//		// GET: /article/add
//		public ViewResult Add()
//		{
//			return this.View();
//		}

//		// POST: /article/add
//		[HttpPost, ValidateAntiForgeryToken]
//		public ActionResult Add(EditArticleViewModel model)
//		{
//			if (ModelState.IsValid)
//			{
//				var article = new ArticleDto
//				{
//					Title = model.Title,
//					Content = model.Content
//				};

//				this.articleService.AddArticle(article);

//				TempData["SuccessMessage"] = string.Format("You have successfully added '{0}' as a new article.", article.Title);

//				return this.RedirectToAction("Index");
//			}

//			return this.View(model);
//		}

//		// GET: /article/edit
//		public ActionResult Edit(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}

//			var response = this.articleService.GetArticle((int)id);

//			if (response.Status == ResponseStatus.OK)
//			{
//				var article = new EditArticleViewModel
//				{
//					Title = response.Article.Title,
//					Content = response.Article.Content
//				};

//				return this.View(article);
//			}
//			else if (response.Status == ResponseStatus.NotFound)
//			{
//				return this.HttpNotFound();
//			}
//			else
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
//			}
//		}

//		// POST: /article/edit
//		[HttpPost, ValidateAntiForgeryToken]
//		public ActionResult Edit(int? id, EditArticleViewModel model)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}

//			if (ModelState.IsValid)
//			{
//				var article = new ArticleDto
//				{
//					Id = (int)id,
//					Title = model.Title,
//					Content = model.Content
//				};

//				this.articleService.UpdateArticle(article);

//				TempData["SuccessMessage"] = string.Format("You have successfully updated '{0}'.", article.Title);

//				return this.RedirectToAction("Index");
//			}

//			return this.View(model);
//		}

//		// POST: /article/delete
//		[HttpPost, ValidateAntiForgeryToken]
//		public RedirectToRouteResult Delete(int id)
//		{
//			var response = this.articleService.DeleteArticle(id);

//			if (response.Status == ResponseStatus.OK)
//			{
//				TempData["SuccessMessage"] = string.Format("You have successfully deleted '{0}'.", response.Title);
//			}
//			else if (response.Status == ResponseStatus.NotFound)
//			{
//				TempData["FailureMessage"] = "The article you are trying to delete cannot be found.";
//			}
//			else
//			{
//				TempData["FailureMessage"] = "The article has not deleted successfully. Please try again.";
//			}

//			return this.RedirectToAction("Index");
//		}
//	}
//}