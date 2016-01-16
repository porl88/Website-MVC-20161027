namespace MVC.Services.Tests.Culture
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Core.Exceptions;
    using Services.Culture;
    using Core.Testing;
    using Core.Entities.Culture;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Services.Culture.Transfer;

    [TestClass]
    public class LanguageServiceTests
    {
        private ILanguageService languageService;

        [TestInitialize]
        public void Init()
        {
            var unitOfWork = this.CreateMockUnitOfWork();
            this.languageService = new LanguageService(unitOfWork, new NullExceptionHandler());
        }

        [TestMethod]
        public async Task GetLanguages()
        {
            // arrange
            var request = new GetLanguagesRequest
            {
                LanguageId = 2
            };

            // act
            var result = await this.languageService.GetLanguagesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var languages = result.Languages;
            Assert.AreEqual(3, languages.Count);

            var language1 = languages[0];
            Assert.AreEqual(2, language1.Id);
            Assert.AreEqual("Deutsche", language1.Name);

            var language2 = languages[1];
            Assert.AreEqual(1, language2.Id);
            Assert.AreEqual("Englisch", language2.Name);

            var language3 = languages[2];
            Assert.AreEqual(3, language3.Id);
            Assert.AreEqual("Französisch", language3.Name);
        }

        [TestMethod]
        public async Task GetLanguages_Default()
        {
            // arrange
            var request = new GetLanguagesRequest();

            // act
            var result = await this.languageService.GetLanguagesAsync(request);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);

            var languages = result.Languages;
            Assert.AreEqual(3, languages.Count);

            var language1 = languages[0];
            Assert.AreEqual(1, language1.Id);
            Assert.AreEqual("English", language1.Name);

            var language2 = languages[1];
            Assert.AreEqual(3, language2.Id);
            Assert.AreEqual("French", language2.Name);

            var language3 = languages[2];
            Assert.AreEqual(2, language3.Id);
            Assert.AreEqual("German", language3.Name);
        }

        private MockUnitOfWork CreateMockUnitOfWork()
        {
            var unitOfWork = new MockUnitOfWork();
            var now = DateTimeOffset.Now.AddDays(-4);

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 1,
                LanguageCode = "en-gb",
                LanguageVersions = new List<LanguageVersion>
                {
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 1,
                        LanguageId = 1,
                        Language = 1,
                        Name = "English",
                        Dialect = "British",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 2,
                        LanguageId = 1,
                        Language = 2,
                        Name = "Englisch",
                        Dialect = "Britisches",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 3,
                        LanguageId = 1,
                        Language = 3,
                        Name = "Anglais",
                        Dialect = "Britannique",
                        Created = now,
                        Updated = now
                    })
                },
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 2,
                LanguageCode = "de-de",
                LanguageVersions = new List<LanguageVersion>
                {
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 4,
                        LanguageId = 2,
                        Language = 1,
                        Name = "German",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 5,
                        LanguageId = 2,
                        Language = 2,
                        Name = "Deutsche",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 6,
                        LanguageId = 2,
                        Language = 3,
                        Name = "Allemand",
                        Created = now,
                        Updated = now
                    })
                },
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 3,
                LanguageCode = "fr-fr",
                LanguageVersions = new List<LanguageVersion>
                {
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 7,
                        LanguageId = 3,
                        Language = 1,
                        Name = "French",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 8,
                        LanguageId = 3,
                        Language = 2,
                        Name = "Französisch",
                        Created = now,
                        Updated = now
                    }),
                    unitOfWork.LanguageVersionRepository.Insert(new LanguageVersion
                    {
                        Id = 9,
                        LanguageId = 3,
                        Language = 3,
                        Name = "Francais",
                        Created = now,
                        Updated = now
                    })
                },
                Created = now,
                Updated = now
            });

            return unitOfWork;
        }
    }
}
