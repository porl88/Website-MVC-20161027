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

    public class IdentityAdapter : ILoginService, IAccountService
    {
        public bool IsAuthenticated
        {
            get
            {
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

        public string CreateAccount(string email, string password)
        {
            return this.CreateAccount(email, password, email);
        }

        public string CreateAccount(string userName, string password, string email)
        {
            //// Default UserStore constructor uses the default connection string named: DefaultConnection
            //var userStore = new UserStore<IdentityUser>();
            //var manager = new UserManager<IdentityUser>(userStore);
            //var user = new IdentityUser() { UserName = UserName.Text };

            //IdentityResult result = manager.Create(user, Password.Text);

            //if (result.Succeeded)
            //{
            //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
            //    Response.Redirect("~/Login.aspx");
            //}
            //else
            //{
            //    StatusMessage.Text = result.Errors.FirstOrDefault();
            //}
            throw new NotImplementedException();
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
