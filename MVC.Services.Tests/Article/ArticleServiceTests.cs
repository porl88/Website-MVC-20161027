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
            var unitOfWork = this.CreateMockUnitOfWork();
            this.articleService = new ArticleService(unitOfWork, new NullExceptionHandler());
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
        public async Task AddArticleAsync()
        {
            // arrange
            var id = 4;
            var unitOfWork = this.CreateMockUnitOfWork();
            var articleService = new ArticleService(unitOfWork, new NullExceptionHandler());
            var originalNoteCount = unitOfWork.ArticleVersionRepository.Get().Count();

            var request = new EditArticleRequest
            {
                Article = new ArticleDto
                {
                    Title = "New Article",
                    Content = "ZZZ"
                }
            };

            // act
            var result = await articleService.AddArticleAsync(request);

            // assert
            Assert.IsNotNull(result);

            var addedArticleId = result.Note.Id;
            Assert.IsTrue(addedNoteId > 0);

            var addedNote = unitOfWork.NoteRepository.Get(addedNoteId);
            Assert.IsNotNull(addedNote);
            Assert.AreEqual("New Article", addedNote.Title);
            Assert.AreEqual("ZZZ", addedNote.Content);
            Assert.AreEqual(addedNote.Created, addedNote.Updated);
            Assert.IsTrue(addedNote.Updated > DateTimeOffset.Now.AddMinutes(-1));

            var updatedCount = unitOfWork.NoteRepository.Get();
            Assert.AreEqual(originalNoteCount + 1, updatedCount.Count());
        }

        [TestMethod]
        public async Task UpdateArticleAsync()
        {
            // arrange
            var id = 4;
            var unitOfWork = this.CreateMockUnitOfWork();
            var articlesService = new ArticleService(unitOfWork, new NullExceptionHandler());
            var originalNoteCount = unitOfWork.ArticleVersionRepository.Get().Count();
            var originalNote = unitOfWork.ArticleVersionRepository.Get(id);
            var originalNoteCreated = originalNote.Created;
            var originalNoteUpdated = originalNote.Updated;

            var request = new EditArticleRequest
            {
                Article = new ArticleDto
                {
                    Id = id,
                    Title = "Hello",
                    Content = "XXX"
                }
            };

            // act
            var result = await articleService.UpdateArticleAsync(request);

            // assert
            Assert.IsNotNull(result);

            var updatedNote = unitOfWork.ArticleVersionRepository.Get(id);
            Assert.IsNotNull(updatedNote);
            Assert.AreEqual("Hello", updatedNote.Title);
            Assert.AreEqual("XXX", updatedNote.Content);
            Assert.AreEqual(originalNoteCreated, updatedNote.Created);
            Assert.IsTrue(updatedNote.Updated > originalNoteUpdated);
            Assert.IsTrue(updatedNote.Updated > DateTimeOffset.Now.AddMinutes(-1));

            var updatedCount = unitOfWork.ArticleVersionRepository.Get();
            Assert.AreEqual(originalNoteCount, updatedCount.Count());
        }

        private MockUnitOfWork CreateMockUnitOfWork()
        {
            var unitOfWork = new MockUnitOfWork();
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

            return unitOfWork;
        }
    }
}
