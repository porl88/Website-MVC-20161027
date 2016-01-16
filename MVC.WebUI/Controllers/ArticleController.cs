namespace MVC.WebUI.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Models.Article;
    using Services.Culture;
    using Services;
    using Services.Article;
    using Services.Article.Transfer;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService, ILanguageService languageService)
            :base(languageService)
        {
            this.articleService = articleService;
        }

        // GET: /article
        public async Task<ViewResult> Index()
        {
            var response = await this.articleService.GetArticlesAsync(new GetArticlesRequest
            {
                LanguageId = base.LanguageId
            });

            var model = new ArticleIndexViewModel
            {
                Articles = response.Articles
            };

            return View(model);
        }

        // GET: /article/index-edit
        [ActionName("index-edit")]
        [Authorize]
        public async Task<ViewResult> IndexEdit()
        {
            var response = await this.articleService.GetEditArticlesAsync(new GetArticlesRequest
            {
                LanguageId = base.LanguageId
            });

            var model = new ArticleIndexViewModel
            {
                Articles = response.Articles
            };

            return View("index-edit", model);
        }

        // GET: /article/details/1
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.HttpNotFound();
            }

            var response = await this.articleService.GetArticleAsync(new GetArticleRequest
            {
                ArticleId = (int)id
            });

            if (response.Status == ResponseStatus.OK)
            {
                var article = new ArticleDetailsViewModel
                {
                    Article = response.Article
                };

                return View(article);
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

        // GET: /article/create
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        // POST: /article/create
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Article.LanguageId = 1;
                var response = await this.articleService.AddArticleAsync(model.Article);
                if (response.Status == ResponseStatus.OK)
                {
                    TempData["SuccessMessage"] = string.Format("You have successfully added '{0}' as a new article.", response.Article.Title);
                }
                else
                {
                    TempData["FailureMessage"] = "An error has occurred and the article has not been created.";
                }

                return this.RedirectToAction("Index");
            }

            return View();
        }

        // GET: /article/edit/1
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await this.articleService.GetEditArticleAsync(new GetArticleRequest
            {
                ArticleId = id
            });

            if (response.Status == ResponseStatus.OK)
            {
                var model = new ArticleEditViewModel
                {
                    Article = response.Article,
                    Languages = this.GetLanguageListItems()
                };

                return View(model);
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

        // POST: /article/edit/1
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ArticleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await this.articleService.UpdateArticleAsync(model.Article);

                if (response.Status == ResponseStatus.OK)
                {
                    TempData["SuccessMessage"] = string.Format("You have successfully updated '{0}'.", response.Article.Title);
                }
                else
                {
                    TempData["FailureMessage"] = "An error has occurred and the article has not been updated.";
                }

                return this.RedirectToAction("Index");
            }

            return View();
        }

        // POST: /article/delete/1
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> Delete(int id)
        {
            var request = new DeleteArticleRequest
            {
                ArticleId = id
            };

            var response = await this.articleService.DeleteArticleAsync(request);

            if (response.Status == ResponseStatus.OK)
            {
                TempData["SuccessMessage"] = string.Format("You have successfully deleted '{0}'.", response.Title);
            }
            else if (response.Status == ResponseStatus.NotFound)
            {
                TempData["FailureMessage"] = "The article you are trying to delete cannot be found.";
            }
            else
            {
                TempData["FailureMessage"] = "The article has not deleted successfully. Please try again.";
            }

            return this.RedirectToAction("Index");
        }

        private List<SelectListItem> GetLanguageListItems()
        {
            var languages = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "English (American)",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "English (British)",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "German",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "French",
                    Value = "3"
                }
            };

            return languages;
        }
    }
}