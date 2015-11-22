namespace MVC.Services.Tests.Page
{
    using System.Threading.Tasks;
    using Core.Entities.Website;
    using Core.Exceptions;
    using Core.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Page;

    [TestClass]
    public class PageServiceTests
    {
        [TestMethod]
        public void GetPage_DefaultLanguage_Success()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = pageService.GetPage(8);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var page = result.Page;
            Assert.AreEqual("Hello", page.Title);
            Assert.AreEqual("A nice page.", page.Description);
            Assert.AreEqual("car, tree", page.Keywords);
        }

        [TestMethod]
        public void GetPage_SelectedLanguage_Success()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = pageService.GetPage(8, "de-de");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var page = result.Page;
            Assert.AreEqual("Achtung!", page.Title);
            Assert.AreEqual("Das is goot!", page.Description);
            Assert.AreEqual("kraftwerk", page.Keywords);
        }

        [TestMethod]
        public void GetPage_DefaultLanguage_NotFound()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = pageService.GetPage(2);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        [TestMethod]
        public void GetPage_SelectedLanguage_NotFound()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = pageService.GetPage(8, "pd-fu");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        [TestMethod]
        public async Task GetPageAsync_DefaultLanguage_Success()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = await pageService.GetPageAsync(8);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var page = result.Page;
            Assert.AreEqual("Hello", page.Title);
            Assert.AreEqual("A nice page.", page.Description);
            Assert.AreEqual("car, tree", page.Keywords);
        }

        [TestMethod]
        public async Task GetPageAsync_SelectedLanguage_Success()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = await pageService.GetPageAsync(8, "de-de");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.OK, result.Status);
            var page = result.Page;
            Assert.AreEqual("Achtung!", page.Title);
            Assert.AreEqual("Das is goot!", page.Description);
            Assert.AreEqual("kraftwerk", page.Keywords);
        }

        [TestMethod]
        public async Task GetPageAsync_DefaultLanguage_NotFound()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = await pageService.GetPageAsync(2);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        [TestMethod]
        public async Task GetPageAsnyc_SelectedLanguage_NotFound()
        {
            // arrange
            var pageService = this.CreatePageService();

            // act
            var result = await pageService.GetPageAsync(8, "pd-fu");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.NotFound, result.Status);
        }

        private PageService CreatePageService()
        {
            var unitOfWork = new MockUnitOfWork();
            var pageService = new PageService(unitOfWork, new NullExceptionHandler());

            unitOfWork.PageRepository.Insert(new Page
            {
                Id = 8
            });

            unitOfWork.PageVersionRepository.Insert(new PageVersion
            {
                Id = 4,
                PageId = 8,
                LanguageCode = "en-gb",
                Title = "Hello",
                Description = "A nice page.",
                Keywords = "car, tree"
            });

            unitOfWork.PageVersionRepository.Insert(new PageVersion
            {
                Id = 5,
                PageId = 8,
                LanguageCode = "de-de",
                Title = "Achtung!",
                Description = "Das is goot!",
                Keywords = "kraftwerk"
            });

            return pageService;
        }
    }
}
