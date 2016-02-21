﻿namespace MVC.Services.Account
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using Core.Data.EntityFramework;
    using Core.Entities.Account;
    using Core.Exceptions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Transfer;

    public class IdentityAdapter : IAuthenticationService, IAccountService
    {
        private readonly IExceptionHandler exceptionHandler;
        private readonly IDataProtectionProvider protectionProvider;
        private readonly IOwinContext context;

        public IdentityAdapter(IExceptionHandler exceptionHandler)
        {
            this.exceptionHandler = exceptionHandler;
            this.protectionProvider = new DpapiDataProtectionProvider("WebsiteMVC");
            this.context = HttpContext.Current.GetOwinContext();
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.context.Authentication.User.Identity.IsAuthenticated;
            }
        }

        public LoginResponse LogIn(LoginRequest request)
        {
            var response = new LoginResponse();
            //var user = await UserManager.FindAsync(model.UserName, model.Password);

            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);

            var authenticationProperties = new AuthenticationProperties();

            if (request.Persistence != null)
            {
                authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.Add((TimeSpan)request.Persistence);
            }

            this.context.Authentication.SignIn(authenticationProperties);
            // SignInStatus 

            return response;
        }

        //public bool LogIn(string userName, string password)
        //{
        //    using (var userStore = new UserStore<IdentityUser>())
        //    {
        //        using (var userManager = new UserManager<IdentityUser>(userStore))
        //        {
        //            var user = userManager.Find(userName, password);
        //            if (user != null)
        //            {
        //                //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //    //var userStore = new UserStore<IdentityUser>();
        //    //var userManager = new UserManager<IdentityUser>(userStore);
        //    //var user = userManager.Find(UserName.Text, Password.Text);

        //    //if (user != null)
        //    //{
        //    //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //    //    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

        //    //    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
        //    //    Response.Redirect("~/Login.aspx");
        //    //}
        //    //else
        //    //{
        //    //    StatusText.Text = "Invalid username or password.";
        //    //    LoginStatus.Visible = true;
        //    //}

        //    throw new NotImplementedException();
        //}

        public void LogOut()
        {
            this.context.Authentication.SignOut();
        }

        public bool ActivateAccount(string activateAccountToken)
        {
            // http://bitoftech.net/2015/02/03/asp-net-identity-2-accounts-confirmation-password-user-policy-configuration/
            // http://www.asp.net/identity/overview/features-api/account-confirmation-and-password-recovery-with-aspnet-identity
            throw new NotImplementedException();
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public CreateAccountResponse CreateAccount(CreateAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            var response = new CreateAccountResponse();

            //var user = new ApplicationUser() { UserName = model.UserName };
            //var result = await UserManager.CreateAsync(user, model.Password);

            try
            {
                using (var context = new WebsiteDbContext())
                {
                    using (var userStore = new UserStore<IdentityUser>(context))
                    {
                        using (var userManager = new UserManager<IdentityUser>(userStore))
                        {
                            var user = new IdentityUser
                            {
                                UserName = request.UserName,
                                Email = request.Email
                            };

                            var result = await userManager.CreateAsync(user, request.Password);

                            if (result.Succeeded)
                            {
                                userManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(this.protectionProvider.Create("EmailConfirmation"));
                                response.ActivateAccountToken = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                response.Status = StatusCode.OK;
                            }
                            else
                            {
                                response.Status = StatusCode.BadRequest;
                                //response.Message = string.Join(". ", result.Errors);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        public ResetPasswordRequestResponse ResetPasswordRequest(ResetPasswordRequestRequest request)
        {
            throw new NotImplementedException();
        }

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public ActivateAccountResponse ActivateAccount(ActivateAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
