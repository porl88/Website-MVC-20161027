namespace MVC.WebUI.Controllers
{
    using System;
    using System.Web.Mvc;
    using MVC.Services.Account;
    using Services.Account.Transfer;
    using MVC.Services.Message;
    using MVC.WebUI.Attributes;
    using MVC.WebUI.Models.Account;
    using Core.Configuration;
    using Services;

    [Authorize]
	public class AccountController : Controller
	{
		private readonly ILoginService loginService;

		private readonly IAccountService accountService;

		private readonly IMessageService messageService;

		public AccountController(ILoginService loginService, IAccountService accountService, IMessageService messageService)
		{
			this.loginService = loginService;
			this.accountService = accountService;
			this.messageService = messageService;
		}

		// GET: /account
		public ViewResult Index()
		{
			return this.View();
		}

		// GET: /account/create-account
		[ActionName("create-account")]
		[AllowAnonymous]
		public ActionResult CreateAccount()
		{
			if (this.loginService.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }
            
            return this.View();
		}

		// POST: /account/create-account
		[ActionName("create-account")]
		[AllowAnonymous, HttpPost, ValidateAntiForgeryToken, ValidateHttpReferrer]
		public ViewResult CreateAccount(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
                var request = new CreateAccountRequest
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email
                };

				var response = this.accountService.CreateAccount(request);

                if (response.Status == ResponseStatus.OK)
                {
                    // sent out activation token
                    // redirect to login page
                    // display success message

                    //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    //    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                    //    Response.Redirect("~/Login.aspx");

                    this.messageService.SendMessage(new MessageRequest
                    {
                        ToAddress = model.Email,
                        Subject = "Please confirm your account with " + WebsiteConfig.WebsiteUrl,
                        Message = string.Empty
                    });
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Your account was not created for the following reason(s): " + response.Message);
                }
			}

			return this.View();
		}

		// GET: /account/login
		[AllowAnonymous]
		public ActionResult LogIn(string returnUrl)
		{
			if (this.loginService.IsAuthenticated)
			{
				return this.RedirectToAction("Index", "Home");
			}

			ViewBag.ReturnUrl = returnUrl;

			return this.View();
		}

		// POST: /account/login
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public ActionResult LogIn(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid && this.loginService.LogIn(model.UserName, model.Password))
			{
                return this.SecureRedirect(returnUrl);
			}

			ModelState.AddModelError("LoginFailure", "Your user name and/or password are incorrect.");

			return this.View();
		}

		// GET: /account/logout
		[AllowAnonymous]
		public RedirectToRouteResult LogOut()
		{
			this.loginService.LogOut();
			return this.RedirectToAction("Index", "Home");
		}

		// GET: /account/change-password
		[ActionName("change-password")]
		public ViewResult ChangePassword()
		{
			return this.View();
		}

		// POST: /account/change-password
		[ActionName("change-password")]
		[HttpPost, ValidateAntiForgeryToken, ValidateHttpReferrer]
		public ActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (this.accountService.ChangePassword(model.OldPassword, model.NewPassword))
				{
					this.TempData["SuccessMessage"] = "You have successfully changed your password.";
					return this.RedirectToAction("index");
				}
				else
				{
					ViewBag.DisplaySummary = "yes";
					ModelState.AddModelError(string.Empty, "Your old password has not been recognised. Please try again.");
				}
			}

			return this.View();
		}

		// GET: /account/request-password
		[AllowAnonymous]
		[ActionName("request-password")]
		public ActionResult RequestPassword()
		{
			if (this.loginService.IsAuthenticated)
            {
				return this.RedirectToAction("index");
            }

			return this.View();
		}

		// POST: /account/request-password
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		[ActionName("request-password")]
		public ActionResult RequestPassword(RequestPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var expires = new TimeSpan(2, 0, 0);
				var resetPasswordToken = this.accountService.ResetPasswordRequest(model.UserName, expires);
				var message = new MessageRequest
				{
					ToAddress = string.Empty,
					Subject = "You have requested to reset your password",
					Message = resetPasswordToken
				};
				this.messageService.SendMessage(message);
				this.TempData["SuccessMessage"] = "You have successfully requested a new password. You should receive an email presently with instructions on how to reset your password. If you do not receive an email within 15 minutes, please check that you have the correct user name and try again." + resetPasswordToken;
				return this.RedirectToAction("login");
			}

			return this.View();
		}

		// GET: /account/reset-password
		[AllowAnonymous]
		[ActionName("reset-password")]
		public ActionResult ResetPassword(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				this.TempData.Add("ResetPasswordFail", "XXX");
				return this.RedirectToAction("request-password");
			}

			return this.View();
		}

		// POST: /account/reset-password
		[ActionName("reset-password")]
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken, ValidateHttpReferrer]
		public ActionResult ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (this.accountService.ResetPassword(model.ResetPasswordToken, model.NewPassword))
				{
					this.TempData["SuccessMessage"] = "You have successfully reset your password. Please login with your new credentials.";
					return this.RedirectToAction("login");
				}
				else
				{
					TempData.Add("ResetPasswordFail", "XXX");
					return this.RedirectToAction("request-password");
				}
			}

			return this.View();
		}

		private ActionResult SecureRedirect(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return this.Redirect(returnUrl);
			}
			else
			{
				return this.RedirectToAction("Index", "Home");
			}
		}
	}
}