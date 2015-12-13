namespace MVC.WebUI.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using MVC.Services;
    using MVC.Services.Article;
    using MVC.Services.Article.Transfer;
    using Models;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        // GET: /article
        public async Task<ViewResult> Index()
        {
            var articles = await this.articleService.GetArticlesAsync();
            return this.View(articles);
        }

        // GET: /article/details
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var request = new GetArticleRequest
            {
                ArticleId = (int)id
            };

            var response = await this.articleService.GetArticleAsync(request);

            if (response.Status == ResponseStatus.OK)
            {
                var model = new ArticleDetailsViewModel
                {
                    Article = response.Article
                };

                return this.View(model);
            }
            else if (response.Status == ResponseStatus.NotFound)
            {
                return this.HttpNotFound();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        //// GET: /article/add
        //public ViewResult Add()
        //{
        //    return this.View();
        //}

        //// POST: /article/add
        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult Add(EditArticleViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var article = new ArticleDto
        //        {
        //            Title = model.Title,
        //            Content = model.Content
        //        };

        //        this.articleService.AddArticle(article);

        //        TempData["SuccessMessage"] = string.Format("You have successfully added '{0}' as a new article.", article.Title);

        //        return this.RedirectToAction("Index");
        //    }

        //    return this.View(model);
        //}

        // GET: /article/edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var request = new GetArticleRequest
            {
                ArticleId = (int)id
            };

            var response = await this.articleService.GetArticleAsync(request);

            if (response.Status == ResponseStatus.OK)
            {
                var model = new ArticleEditViewModel
                {
                    Id = (int)id,
                    Title = response.Article.Title,
                    Content = response.Article.Content
                };

                return this.View(model);
            }
            else if (response.Status == ResponseStatus.NotFound)
            {
                return this.HttpNotFound();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: /article/edit
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ArticleEditViewModel model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var request = new EditArticleRequest
                {
                    Article = new ArticleDto
                    {
                        ArticleVersionId = (int)id,
                        Title = model.Title,
                        Content = model.Content
                    }
                };

                var response = this.articleService.UpdateArticle(request);

                TempData["SuccessMessage"] = string.Format("You have successfully updated '{0}'.", response.Article.Title);

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        //// POST: /article/delete
        //[HttpPost, ValidateAntiForgeryToken]
        //public RedirectToRouteResult Delete(int id)
        //{
        //    var response = this.articleService.DeleteArticle(id);

        //    if (response.Status == ResponseStatus.OK)
        //    {
        //        TempData["SuccessMessage"] = string.Format("You have successfully deleted '{0}'.", response.Title);
        //    }
        //    else if (response.Status == ResponseStatus.NotFound)
        //    {
        //        TempData["FailureMessage"] = "The article you are trying to delete cannot be found.";
        //    }
        //    else
        //    {
        //        TempData["FailureMessage"] = "The article has not deleted successfully. Please try again.";
        //    }

        //    return this.RedirectToAction("Index");
        //}
    }
}