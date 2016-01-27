namespace MVC.Services.Account
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Core.Exceptions;
    using Transfer;

    /// <summary>
    /// Login service. Use when storing credentials in the web.config file.
    /// </summary>
    public class FormsAuthenticationAdapter : IAuthenticationService
	{
        private readonly HttpContext context;
        private readonly IExceptionHandler exceptionHandler;

        public FormsAuthenticationAdapter(IExceptionHandler exceptionHandler)
        {
            this.context = HttpContext.Current;
            this.exceptionHandler = exceptionHandler;
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.context.User.Identity.IsAuthenticated;
            }
        }

        public LogInResponse LogIn(LogInRequest request)
		{
            var response = new LogInResponse();

            try
            {
                var persist = false;

                if (request.Persistence != null)
                {
                    persist = true;
                    this.context.Response.Cookies[0].Expires = DateTime.Now.Add(request.Persistence);
                }

                if (FormsAuthentication.Authenticate(request.UserName, request.Password))
                {
                    FormsAuthentication.SetAuthCookie(request.UserName, persist);
                    response.Status = StatusCode.OK;
                }
                else
                {
                    // ???
                }
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
		}

		public void LogOut()
		{
			FormsAuthentication.SignOut();
		}
    }
}
