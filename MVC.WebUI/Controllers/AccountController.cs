﻿namespace MVC.WebUI.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Core.Configuration;
    using Core.Exceptions;
    using Errors;
    using MVC.Services.Account;
    using MVC.Services.Message;
    using MVC.WebUI.Attributes;
    using MVC.WebUI.Models.Account;
    using Services.Account.Transfer;

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IAccountService accountService;
        private readonly IMessageService messageService;

        public AccountController(IAuthenticationService authenticationService, IAccountService accountService, IMessageService messageService)
        {
            this.authenticationService = authenticationService;
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
            if (this.authenticationService.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        // POST: /account/create-account
        [ActionName("create-account")]
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken, ValidateHttpReferrer]
        public ActionResult CreateAccount(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var request = new CreateAccountRequest
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email
                };

                var response = this.accountService.CreateAccount(request);

                if (response.Status == StatusCode.OK)
                {
                    this.SendActivateAccountToken(model.Email, response.ActivateAccountToken);
                    this.TempData["SuccessMessage"] = "You have successfully created a new account. An activation code has been sent to you by email. When you receive the this email, click on the link to activate your account.";
                    return this.RedirectToAction("LogIn");
                }
                else if (response.Status == StatusCode.BadRequest)
                {
                    this.ModelState.AddModelError(string.Empty, "Your account was not created for the following reason: " + ErrorMessageHelper.GetErrorMessage(response.CreateAccountStatus));
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, ErrorMessageHelper.GetErrorMessage(StatusCode.InternalServerError));
                }
            }

            return this.View();
        }

        // GET: /account/login
        [AllowAnonymous]
        public ViewResult LogIn(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        // POST: /account/login
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var request = new LoginRequest
                {
                    UserName = model.UserName,
                    Password = model.Password
                };

                if (model.Persist)
                {
                    request.Persistence = TimeSpan.FromDays(7);
                }

                var response = this.authenticationService.LogIn(request);

                if (response.Status == StatusCode.OK)
                {
                    return this.SecureRedirect(returnUrl);
                }
                else if (response.Status == StatusCode.Unauthorized)
                {
                    this.ModelState.AddModelError(string.Empty, "Your user name and/or password are incorrect.");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, ErrorMessageHelper.GetErrorMessage(StatusCode.InternalServerError));
                }
            }

            return this.View();
        }

        // GET: /account/logout
        public RedirectToRouteResult LogOut()
        {
            this.authenticationService.LogOut();
            this.TempData["SuccessMessage"] = "You have successfully logged out.";
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
            if (this.ModelState.IsValid)
            {
                var request = new ChangePasswordRequest
                {
                    UserName = "???",
                    OldPassword = model.OldPassword,
                    NewPassword = model.NewPassword
                };

                var response = this.accountService.ChangePassword(request);
                if (response.Status == StatusCode.OK)
                {
                    this.TempData["SuccessMessage"] = "You have successfully changed your password.";
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.ViewBag.DisplaySummary = "yes";
                    this.ModelState.AddModelError(string.Empty, "Your old password has not been recognised. Please try again.");
                }
            }

            return this.View();
        }

        // GET: /account/request-password
        [AllowAnonymous]
        [ActionName("request-password")]
        public ViewResult RequestPassword()
        {
            return this.View();
        }

        // POST: /account/request-password
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [ActionName("request-password")]
        public ActionResult RequestPassword(RequestPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var request = new RequestPasswordRequest
                {
                    UserName = model.UserName,
                    Expires = TimeSpan.FromHours(2)
                };

                var response = this.authenticationService.ResetPasswordRequest(request);
                if (response.Status == StatusCode.OK)
                {
                    var message = new MessageRequest
                    {
                        ToAddress = string.Empty,
                        Subject = "You have requested to reset your password",
                        Message = response.ResetPasswordToken
                    };
                    this.messageService.SendMessage(message);
                    this.TempData["SuccessMessage"] = "You have successfully requested a new password. You should receive an email presently with instructions on how to reset your password. If you do not receive an email within 15 minutes, please check that you have the correct user name and try again.";
                    return this.RedirectToAction("Login");
                }
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
            if (this.ModelState.IsValid)
            {
                var request = new ResetPasswordRequest
                {
                    ResetPasswordToken = model.ResetPasswordToken,
                    NewPassword = model.NewPassword
                };

                var response = this.authenticationService.ResetPassword(request);

                if (response.Status == StatusCode.OK)
                {
                    this.TempData["SuccessMessage"] = "You have successfully reset your password. Please login with your new credentials.";
                    return this.RedirectToAction("login");
                }
                else
                {
                    this.TempData.Add("ResetPasswordFail", "XXX");
                    return this.RedirectToAction("request-password");
                }
            }

            return this.View();
        }

        private ActionResult SecureRedirect(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        private void SendActivateAccountToken(string email, string activateAccountToken)
        {
            var activateUrl = new UriBuilder
            {
                Scheme = "https",
                Host = this.Request.Url.DnsSafeHost,
                Path = "/account/login",
                Query = "id=" + HttpUtility.UrlEncode(activateAccountToken)
            };

            var filePath = "/content/html/emails/activate-account.html";
            var message = System.IO.File.ReadAllText(Server.MapPath(filePath));
            message = message
                .Replace("##Name##", "XXX")
                .Replace("##DomainName##", WebsiteConfig.WebsiteUrl)
                .Replace("##ActivateUrl##", activateUrl.ToString());

            this.messageService.SendMessage(new MessageRequest
            {
                ToAddress = email,
                Subject = "Please activate your account with " + WebsiteConfig.WebsiteUrl,
                Message = message
            });
        }
    }
}