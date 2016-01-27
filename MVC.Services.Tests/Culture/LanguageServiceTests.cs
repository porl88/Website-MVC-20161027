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
            this.languageService = new LanguageService(unitOfWork, new NullExceptionHandler(), null);
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
            Assert.AreEqual(StatusCode.OK, result.Status);

            var languages = result.Languages;
            Assert.AreEqual(3, languages.Count);

            var language1 = languages[0];
            Assert.AreEqual(2, language1.Id);
            Assert.AreEqual("Deutsche", language1.Name);

            var language2 = languages[1];
            Assert.AreEqual(1, language2.Id);
            Assert.AreEqual("English", language2.Name);

            var language3 = languages[2];
            Assert.AreEqual(3, language3.Id);
            Assert.AreEqual("Français", language3.Name);
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
            Assert.AreEqual(StatusCode.OK, result.Status);

            var languages = result.Languages;
            Assert.AreEqual(3, languages.Count);

            var language1 = languages[0];
            Assert.AreEqual(2, language1.Id);
            Assert.AreEqual("Deutsche", language1.Name);

            var language2 = languages[1];
            Assert.AreEqual(1, language2.Id);
            Assert.AreEqual("English", language2.Name);

            var language3 = languages[2];
            Assert.AreEqual(3, language3.Id);
            Assert.AreEqual("Français", language3.Name);
        }

        private MockUnitOfWork CreateMockUnitOfWork()
        {
            var unitOfWork = new MockUnitOfWork();
            var now = DateTimeOffset.Now.AddDays(-4);

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 1,
                Name = "English",
                DialectName = "British",
                LocalName = "English",
                LocalDialectName = "British",
                LanguageCode = "en-gb",
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 2,
                LanguageCode = "de-de",
                Name = "German",
                LocalName = "Deutsche",
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 3,
                LanguageCode = "fr-fr",
                Name = "French",
                LocalName = "Français",
                Created = now,
                Updated = now
            });

            unitOfWork.LanguageRepository.Insert(new Language
            {
                Id = 4,
                LanguageCode = "es-es",
                Name = "Spanish",
                LocalName = "Español",
                Deactivated = true,
                Created = now,
                Updated = now
            });

            return unitOfWork;
        }
    }
}
