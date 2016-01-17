namespace MVC.Services.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityAdapter : ILoginService
    {
        public bool IsAuthenticated
        {
            get
            {
                //var userStore = new UserStore<IdentityUser>();
                throw new NotImplementedException();
            }
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
            throw new NotImplementedException();
        }
    }
}
