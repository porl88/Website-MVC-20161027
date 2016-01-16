namespace MVC.Services.Tests.Article
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Entities.Article;
    using Core.Entities.Culture;
    using Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Core.Testing;
    using Services.Article;
    using Services.Article.Transfer;

    [TestClass]
    public class ArticleServiceTests
    {
        private IArticleService articleService;

        [TestInitialize]
        public void Init()
        {
            var unitOfWork = this.CreateMockUnitOfWork();
            this.articleService = new ArticleService(unitOfWork, new NullExceptionHandler());
        }

        [TestMethod]
        public async Task GetArticlesAsync()
        {
            // arrange
            var request = new GetArticlesRequest
            {
                LanguageId = 3
            };

            // act
            var result = await this.articleService.GetArticlesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var articles = result.Articles;
            Assert.IsNotNull(articles);
            Assert.AreEqual(2, articles.Count);

            var article1 = articles[0];
            Assert.AreEqual(1, article1.Id);
            Assert.AreEqual("Bonjour Madam", article1.Title);

            var article2 = articles[1];
            Assert.AreEqual(2, article2.Id);
            Assert.AreEqual("Mange Dieu", article2.Title);
        }

        [TestMethod]
        public async Task GetEditArticlesAsync()
        {
            // arrange
            var request = new GetArticlesRequest
            {
                LanguageId = 3
            };

            // act
            var result = await this.articleService.GetEditArticlesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var articles = result.Articles;
            Assert.IsNotNull(articles);
            Assert.AreEqual(3, articles.Count);

            var article1 = articles[0];
            Assert.AreEqual(1, article1.Id);
            Assert.AreEqual("Bonjour Madam", article1.Title);

            var article2 = articles[1];
            Assert.AreEqual(2, article2.Id);
            Assert.AreEqual("Mange Dieu", article2.Title);

            var article3 = articles[2];
            Assert.AreEqual(3, article3.Id);
            Assert.AreEqual("Wuthering Heights", article3.Title);
        }

        [TestMethod]
        public async Task GetArticlesAsync_DefaultLanguage()
        {
            // arrange
            var request = new GetArticlesRequest();

            // act
            var result = await this.articleService.GetArticlesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var articles = result.Articles;
            Assert.IsNotNull(articles);
            Assert.AreEqual(3, articles.Count);

            var article1 = articles[0];
            Assert.AreEqual(2, article1.Id);
            Assert.AreEqual("Great Expectations", article1.Title);

            var article2 = articles[1];
            Assert.AreEqual(1, article2.Id);
            Assert.AreEqual("Middlemarch", article2.Title);

            var article3 = articles[2];
            Assert.AreEqual(3, article3.Id);
            Assert.AreEqual("Wuthering Heights", article3.Title);
        }

        [TestMethod]
        public async Task GetArticlesAsync_ContainsUnpublishedArticles()
        {
            // arrange
            var request = new GetArticlesRequest
            {
                LanguageId = 2
            };

            // act
            var result = await this.articleService.GetArticlesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var articles = result.Articles;
            Assert.IsNotNull(articles);
            Assert.AreEqual(1, articles.Count);
        }

        [TestMethod]
        public async Task GetArticleAsync()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 2,
                LanguageId = 2
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual(2, article.Id);
            Assert.AreEqual("Mein Kampf", article.Title);
            Assert.AreEqual("Achtung!", article.Content);
        }

        [TestMethod]
        public async Task GetArticleAsync_DefaultLanguage()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 2
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var article = result.Article;
            Assert.IsNotNull(article);
            Assert.AreEqual(2, article.Id);
            Assert.AreEqual("Great Expectations", article.Title);
            Assert.AreEqual("Rubbish", article.Content);
        }

        [TestMethod]
        public async Task GetArticleAsync_ArticleNotFound()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 55
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        [TestMethod]
        public async Task GetArticleAsync_ArticleVersionNotFound()
        {
            // arrange
            var request = new GetArticleRequest
            {
                ArticleId = 1,
                LanguageId = 2
            };

            // act
            var result = await this.articleService.GetArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        [TestMethod]
        public async Task AddArticleAsync()
        {
            // arrange
            var unitOfWork = this.CreateMockUnitOfWork();
            var articleService = new ArticleService(unitOfWork, new NullExceptionHandler());

            var request = new ArticleEditDto
            {
                LanguageId = 2,
                Title = "Title",
                Content = "Content",
                Publish = true
            };

            // act
            var result = await this.articleService.AddArticleAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var article = result.Article;
            Assert.AreEqual(4, article.ArticleId);
            Assert.AreEqual(8, article.ArticleVersionId);
            Assert.AreEqual("Title", article.Title);
            Assert.AreEqual("Content", article.Content);
        }

        private MockUnitOfWork CreateMockUnitOfWork()
        {
            var unitOfWork = new MockUnitOfWork();
            var now = DateTimeOffset.Now.AddDays(-3);

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 1,
                LanguageCode = "en-gb",
                Name = "English",
                Dialect = "British",
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 2,
                LanguageCode = "de-de",
                Name = "German",
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 3,
                LanguageCode = "fr-fr",
                Name = "French",
                Created = now,
                Updated = now
            });

            unitOfWork.ArticleRepository.Insert(new Article
            {
                Id = 1,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 1,
                        ArticleId = 1,
                        LanguageId = 1,
                        Title = "Middlemarch",
                        Content = "Cheese",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 2,
                        ArticleId = 1,
                        LanguageId = 3,
                        Title = "Bonjour Madam",
                        Content = "Snails",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    })
                },
                Published = now,
                Created = now,
                Updated = now
            });

            unitOfWork.ArticleRepository.Insert(new Article
            {
                Id = 2,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 3,
                        ArticleId = 2,
                        LanguageId = 3,
                        Title = "Mange Dieu",
                        Content = "Oh la la",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 4,
                        ArticleId = 2,
                        LanguageId = 2,
                        Title = "Mein Kampf",
                        Content = "Achtung!",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 5,
                        ArticleId = 2,
                        LanguageId = 1,
                        Title = "Great Expectations",
                        Content = "Rubbish",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    })
                },
                Published = now,
                Created = now,
                Updated = now
            });

            unitOfWork.ArticleRepository.Insert(new Article
            {
                Id = 3,
                ArticleVersions = new List<ArticleVersion>
                {
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 6,
                        ArticleId = 3,
                        LanguageId = 1,
                        Title = "Wuthering Heights",
                        Content = "Heathcliff, it's me!",
                        IsPublished = true,
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.ArticleVersionRepository.Insert(new ArticleVersion
                    {
                        Id = 7,
                        ArticleId = 3,
                        LanguageId = 2,
                        Title = "Kraftwerk",
                        Content = "Klunk!",
                        IsPublished = false,
                        Created = now,
                        Updated = now
                    })
                },
                Published = now,
                Created = now,
                Updated = now
            });

            return unitOfWork;
        }
    }
}