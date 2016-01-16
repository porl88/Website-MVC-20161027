//namespace MVC.WebUI.Tests.Controllers
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Threading.Tasks;
//    using System.Web.Mvc;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using MVC.Core.Entities.Article;
//    using MVC.Core.Entities.Culture;
//    using MVC.Core.Exceptions;
//    using MVC.Core.Testing;
//    using MVC.Services.Article;
//    using MVC.WebUI.Controllers;
//    using MVC.WebUI.Models;

//    [TestClass]
//    public class ArticleControllerTests
//    {
//        private IArticleService articleService;

//        [TestInitialize]
//        public void Init()
//        {
//            this.articleService = this.CreateMockArticleService();
//        }

//        [TestMethod]
//        public async Task Index()
//        {
//            throw new NotImplementedException();
//        }

//        [TestMethod]
//        public async Task Details()
//        {
//            // arrange
//            var id = 3;
//            var controller = new ArticleController(this.articleService);

//            // act
//            var result = await controller.Details(id) as ViewResult;

//            // assert
//            Assert.IsNotNull(result);

//            var model = result.Model as ArticleDetailsViewModel;
//            Assert.IsNotNull(model);

//            var article = model.Article;
//            Assert.IsNotNull(article);
//            Assert.AreEqual("Article 33", article.Title);
//        }

//        [TestMethod]
//        public async Task Details_InvalidId()
//        {
//            // arrange
//            var id = 3282;
//            var controller = new ArticleController(this.articleService);

//            // act
//            var result = await controller.Details(id) as ActionResult;

//            // assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result is HttpNotFoundResult);
//        }

//        [TestMethod]
//        public async Task Details_NoId()
//        {
//            // arrange
//            var controller = new ArticleController(this.articleService);

//            // act
//            var result = await controller.Details(null) as ActionResult;

//            // assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result is HttpNotFoundResult);
//        }

//        private ArticleService CreateMockArticleService()
//        {
//            var unitOfWork = new MockUnitOfWork();
//            var articleService = new ArticleService(unitOfWork, new NullExceptionHandler());
//            var now = DateTimeOffset.Now;

//            var english = new Language
//            {
//                Id = 1,
//                LanguageCode = "en-gb",
//                Name = "English"
//            };

//            var french = new Language
//            {
//                Id = 2,
//                LanguageCode = "fr-fr",
//                Name = "French"
//            };

//            var german = new Language
//            {
//                Id = 3,
//                LanguageCode = "de-de",
//                Name = "German"
//            };

//            unitOfWork.ArticleRepository.Insert(new Article()
//            {
//                Id = 1,
//                ArticleVersions = new List<ArticleVersion>
//                {
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 11,
//                        ArticleId = 1,
//                        Language = german,
//                        Title = "Article 1"
//                    }),
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 12,
//                        ArticleId = 1,
//                        Language = english,
//                        Title = "Article 2"
//                    }),
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 13,
//                        ArticleId = 1,
//                        Language = french,
//                        Title = "Article 3"
//                    })
//                }
//            });

//            unitOfWork.ArticleRepository.Insert(new Article()
//            {
//                Id = 2,
//                ArticleVersions = new List<ArticleVersion>
//                {
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 21,
//                        ArticleId = 2,
//                        Language = english,
//                        Title = "Article 21"
//                    })
//                }
//            });

//            unitOfWork.ArticleRepository.Insert(new Article()
//            {
//                Id = 3,
//                ArticleVersions = new List<ArticleVersion>
//                {
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 31,
//                        ArticleId = 3,
//                        Language = german,
//                        Title = "Article 31"
//                    }),
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 32,
//                        ArticleId = 3,
//                        Language = french,
//                        Title = "Article 32"
//                    }),
//                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
//                    {
//                        Id = 33,
//                        ArticleId = 3,
//                        Language = english,
//                        Title = "Article 33"
//                    })
//                }
//            });

//            return articleService;
//        }
//    }
//}
