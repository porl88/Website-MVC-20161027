namespace MVC.WebUI.Tests.Account
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;
    using System.Web.Mvc;
    using Services.Message;
    using Services.Account;
    using Core.Exceptions;
    using Services.Account.Transfer;

    // http://www.asp.net/mvc/overview/older-versions-1/unit-testing/creating-unit-tests-for-asp-net-mvc-applications-cs

    [TestClass]
    public class AccountControllerTests
    {
        private ILoginService loginService;
        private IAccountService accountService;

        [TestInitialize]
        public void Init()
        {
            this.loginService = this.CreateMockLoginService();
            this.accountService = this.CreateMockAccountService();
        }

        [TestMethod]
        public void LogIn()
        {
            // arrange
            var controller = new AccountController(this.loginService, this.accountService, new NullMessageService());

            // act
            var result = controller.LogIn() as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("LogIn", result.ViewName);
        }

        private class MockLoginService : ILoginService
        {
            private readonly bool isAuthenticated;

            public MockLoginService(bool isAuthenticated)
            {
                this.isAuthenticated = isAuthenticated;
            }

            public bool IsAuthenticated
            {
                get
                {
                    return isAuthenticated;
                }
            }

            public LogInResponse LogIn(LogInRequest request)
            {
                throw new NotImplementedException();
            }

            public void LogOut()
            {
            }
        }

        private SimpleMembershipAdapter CreateMockLoginService()
        {
            var loginService = new SimpleMembershipAdapter(new NullExceptionHandler());
            return loginService;
        }

        private SimpleMembershipAdapter CreateMockAccountService()
        {
            var accountService = new SimpleMembershipAdapter(new NullExceptionHandler());
            return accountService;
        }
    }
}



/*

    using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
namespace SportsStore.UnitTests {
[TestClass]
public class AdminSecurityTests {
[TestMethod]
public void Can_Login_With_Valid_Credentials() {
// Arrange - create a mock authentication provider
Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
// Arrange - create the view model
LoginViewModel model = new LoginViewModel {
UserName = "admin",
Password = "secret"
};
// Arrange - create the controller
AccountController target = new AccountController(mock.Object);
// Act - authenticate using valid credentials
ActionResult result = target.Login(model, "/MyURL");
// Assert
Assert.IsInstanceOfType(result, typeof(RedirectResult));
Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
}
[TestMethod]
public void Cannot_Login_With_Invalid_Credentials() {
// Arrange - create a mock authentication provider
Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
// Arrange - create the view model
LoginViewModel model = new LoginViewModel {
UserName = "badUser",
Password = "badPass"
};
// Arrange - create the controller
AccountController target = new AccountController(mock.Object);
// Act - authenticate using valid credentials
ActionResult result = target.Login(model, "/MyURL");
// Assert
Assert.IsInstanceOfType(result, typeof(ViewResult));
Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
}}
*/
