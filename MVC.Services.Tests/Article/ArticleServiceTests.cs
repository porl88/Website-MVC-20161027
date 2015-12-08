namespace MVC.Services.Tests.Article
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Core.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Article;
    using MVC.Core.Entities.Article;
    using Services.Article.Transfer;
    using Core.Entities.Culture;

    [TestClass]
    public class ArticleServiceTests
    {
        private IArticleService articleService;

        [TestInitialize]
        public void Init()
        {
            this.articleService = this.CreateMockArticleService();
        }

        [TestMethod]
        public void GetArticle()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 3,
                LanguageCode = "fr-fr"
            };

            // act
            var result = this.articleService.GetArticle(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual("Article 32", article.Title);
        }

        [TestMethod]
        public async Task GetArticleAsync()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 3,
                LanguageCode = "fr-fr"
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual("Article 32", article.Title);
        }

        [TestMethod]
        public void GetArticle_DefaultLanguage()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 1
            };

            // act
            var result = this.articleService.GetArticle(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual("Article 2", article.Title);
        }

        [TestMethod]
        public async Task GetArticleAsync_DefaultLanguage()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 1
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual("Article 2", article.Title);
        }

        [TestMethod]
        public void UpdateArticle()
        {
            // arrange
            var articleService = this.CreateMockArticleService();
            var request = new EditArticleRequest
            {
                Article = new ArticleDto
                {
                    ArticleVersionId = 12,
                    Title = "Hello",
                    Content = "XXX"
                }
            };

            // act
            var result = articleService.UpdateArticle(request);

            // assert
            Assert.IsNotNull(result);

            var updatedArticleRequest = articleService.GetArticle(new GetArticleRequest
            {
                ArticleId = 1
            });

            var updatedArticle = updatedArticleRequest.Article;
            Assert.IsNotNull(updatedArticle);
            Assert.AreEqual("Hello", updatedArticle.Title);
            Assert.AreEqual("XXX", updatedArticle.Content);
        }

        [TestMethod]
        public async Task UpdateArticleAsync()
        {
            // arrange
            var articleService = this.CreateMockArticleService();
            var request = new EditArticleRequest
            {
                Article = new ArticleDto
                {
                    ArticleVersionId = 21,
                    Title = "Goodbye",
                    Content = "YYY"
                }
            };

            // act
            var result = await articleService.UpdateArticleAsync(request);

            // assert
            Assert.IsNotNull(result);

            var updatedArticleRequest = articleService.GetArticle(new GetArticleRequest
            {
                ArticleId = 2
            });

            var updatedArticle = updatedArticleRequest.Article;
            Assert.IsNotNull(updatedArticle);
            Assert.AreEqual("Goodbye", updatedArticle.Title);
            Assert.AreEqual("YYY", updatedArticle.Content);
        }

        private ArticleService CreateMockArticleService()
        {
            var unitOfWork = new MockUnitOfWork();
            var articleService = new ArticleService(unitOfWork, new NullExceptionHandler());
            var now = DateTimeOffset.Now;

            var english = new Language
            {
                Id = 1,
                LanguageCode = "en-gb",
                Name = "English"
            };

            var french = new Language
            {
                Id = 2,
                LanguageCode = "fr-fr",
                Name = "French"
            };

            var german = new Language
            {
                Id = 3,
                LanguageCode = "de-de",
                Name = "German"
            };

            unitOfWork.ArticleRepository.Insert(new Article()
            {
                Id = 1,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 11,
                        ArticleId = 1,
                        Language = german,
                        Title = "Article 1"
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 12,
                        ArticleId = 1,
                        Language = english,
                        Title = "Article 2"
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 13,
                        ArticleId = 1,
                        Language = french,
                        Title = "Article 3"
                    })
                }
            });

            unitOfWork.ArticleRepository.Insert(new Article()
            {
                Id = 2,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 21,
                        ArticleId = 2,
                        Language = english,
                        Title = "Article 21"
                    })
                }
            });

            unitOfWork.ArticleRepository.Insert(new Article()
            {
                Id = 3,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 31,
                        ArticleId = 3,
                        Language = german,
                        Title = "Article 31"
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 32,
                        ArticleId = 3,
                        Language = french,
                        Title = "Article 32"
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 33,
                        ArticleId = 3,
                        Language = english,
                        Title = "Article 33"
                    })
                }
            });

            return articleService;
        }
    }
}
