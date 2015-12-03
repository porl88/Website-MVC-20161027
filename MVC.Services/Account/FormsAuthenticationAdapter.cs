namespace MVC.Services.Account
{
    using System;
    using System.Web;
    using System.Web.Security;

    /// <summary>
    /// Login service. Use when storing credentials in the web.config file.
    /// </summary>
    public class FormsAuthenticationAdapter : ILoginService
	{
		public bool LogIn(string userName, string password)
		{
			if (FormsAuthentication.Authenticate(userName, password))
			{
				FormsAuthentication.SetAuthCookie(userName, false);
				return true;
			}

			return false;
		}

		public bool LogIn(string userName, string password, TimeSpan persistence)
		{
			throw new NotImplementedException();
		}

		public void LogOut()
		{
			FormsAuthentication.SignOut();
		}

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
    }
}
