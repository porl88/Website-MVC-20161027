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
    using Models.Account;

    // http://www.asp.net/mvc/overview/older-versions-1/unit-testing/creating-unit-tests-for-asp-net-mvc-applications-cs

    [TestClass]
    public class AccountControllerTests
    {
        private IAuthenticationService loginService;
        private IAccountService accountService;

        [TestInitialize]
        public void Init()
        {
            this.loginService = new MockLoginService();
            this.accountService = this.CreateMockAccountService();
        }

        //[TestMethod]
        //public void LogIn()
        //{
        //    // arrange
        //    var controller = new AccountController(this.loginService, this.accountService, new NullMessageService());

        //    // act
        //    var result = controller.LogIn() as ViewResult;

        //    // assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void LogIn_AlreadyAuthenticated()
        //{
        //    // arrange
        //    var controller = new AccountController(this.loginService, this.accountService, new NullMessageService());
        //    controller.LogIn("Hello", "Sailor");

        //    // act
        //    var result = controller.LogIn() as RedirectToRouteResult;

        //    // assert
        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void LogIn_ValidCredentials()
        {
            // arrange
            var controller = new AccountController(this.loginService, this.accountService, new NullMessageService());
            var returnUrl = string.Empty;
            var model = new LoginViewModel
            {
                UserName = "Hello",
                Password = "Sailor"
            };

            // act
            var result = controller.LogIn(model, returnUrl) as RedirectToRouteResult;

            // assert
            //Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            //Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        private class MockLoginService : IAuthenticationService
        {
            public static bool isAuthenticated;

            public int CurrentUserId
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public string CurrentUserName
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool IsAuthenticated
            {
                get
                {
                    return isAuthenticated;
                }
            }

            public LoginResponse LogIn(LoginRequest request)
            {
                var response = new LoginResponse();

                if (request.UserName == "Hello" && request.Password == "Sailor")
                {
                    isAuthenticated = true;
                    response.Status = StatusCode.OK;
                }
                else
                {
                    response.Status = StatusCode.Unauthorized;
                }

                return response;
            }

            public void LogOut()
            {
                isAuthenticated = false;
            }

            public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
            {
                throw new NotImplementedException();
            }

            public ResetPasswordRequestResponse ResetPasswordRequest(ResetPasswordRequestRequest request)
            {
                throw new NotImplementedException();
            }
        }

        private SimpleMembershipAdapter CreateMockLoginService()
        {
            var loginService = new SimpleMembershipAdapter(null, new NullExceptionHandler());
            return loginService;
        }

        private SimpleMembershipAdapter CreateMockAccountService()
        {
            var accountService = new SimpleMembershipAdapter(null, new NullExceptionHandler());
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
