namespace MVC.Services.Account
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Transfer;

    /// <summary>
    /// Login service. Use when storing credentials in the web.config file.
    /// </summary>
    public class FormsAuthenticationAdapter : ILoginService
	{
		public bool LogIn(LogInRequest request)
		{
			if (FormsAuthentication.Authenticate(request.UserName, request.Password))
			{
                var persitentCookie = request.Persistence != null;
				FormsAuthentication.SetAuthCookie(request.UserName, persitentCookie);
				return true;
			}

			return false;
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
