namespace MVC.Services.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Core.Entities.Account;
    using Core.Data.EntityFramework;
    using Transfer;
    using Core.Exceptions;
    using System.Web;

    public class IdentityAdapter : ILoginService, IAccountService
    {
        private readonly IExceptionHandler exceptionHandler;

        public IdentityAdapter(IExceptionHandler exceptionHandler)
        {
            this.exceptionHandler = exceptionHandler;
        }

        public bool IsAuthenticated
        {
            get
            {
                return false;
                //var userStore = new UserStore<IdentityUser>();
                throw new NotImplementedException();
            }
        }

        public bool ActivateAccount(string activateAccountToken)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public CreateAccountResponse CreateAccount(CreateAccountRequest request)
        {
            var response = new CreateAccountResponse();

            try
            {
                using (var context = new WebsiteDbContext())
                {
                    using (var userStore = new UserStore<IdentityUser>(context))
                    {
                        using (var manager = new UserManager<IdentityUser>(userStore))
                        {
                            var user = new IdentityUser
                            {
                                UserName = request.UserName,
                                Email = request.Email
                            };

                            var result = manager.Create(user, request.Password);

                            if (result.Succeeded)
                            {
                                response.ActivateAccountToken = "??? not here???";
                                response.Status = ResponseStatus.OK;
                            }
                            else
                            {
                                response.Status = ResponseStatus.BadRequest;
                                response.Message = string.Join(". ", result.Errors);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.SystemError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        public void DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public bool LogIn(string userName, string password)
        {
            using (var userStore = new UserStore<IdentityUser>())
            {
                using (var userManager = new UserManager<IdentityUser>(userStore))
                {
                    var user = userManager.Find(userName, password);
                    if (user != null)
                    {
                        //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                        return true;
                    }
                }
            }

            return false;
            //var userStore = new UserStore<IdentityUser>();
            //var userManager = new UserManager<IdentityUser>(userStore);
            //var user = userManager.Find(UserName.Text, Password.Text);

            //if (user != null)
            //{
            //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            //    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
            //    Response.Redirect("~/Login.aspx");
            //}
            //else
            //{
            //    StatusText.Text = "Invalid username or password.";
            //    LoginStatus.Visible = true;
            //}

            throw new NotImplementedException();
        }

        public bool LogIn(string userName, string password, TimeSpan persistence)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //authenticationManager.SignOut();
            //Response.Redirect("~/Login.aspx");
            throw new NotImplementedException();
        }

        public bool ResetPassword(string resetPasswordToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public string ResetPasswordRequest(string userName, TimeSpan expires)
        {
            throw new NotImplementedException();
        }
    }
}
