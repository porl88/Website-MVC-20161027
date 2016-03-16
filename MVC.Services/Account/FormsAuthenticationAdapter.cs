namespace MVC.Services.Account
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Core.Exceptions;
    using Transfer;

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

        public LoginResponse LogIn(LoginRequest request)
        {
            var response = new LoginResponse();

            try
            {
                var persist = false;

                if (request.Persistence != null)
                {
                    //var x = new FormsAuthenticationTicket("authentication", true, (int)request.Persistence.Value.TotalMinutes);
                    persist = true;
                    this.context.Response.Cookies[0].Expires = DateTime.Now.Add((TimeSpan)request.Persistence);
                }

                if (FormsAuthentication.Authenticate(request.UserName, request.Password))
                {
                    FormsAuthentication.SetAuthCookie(request.UserName, persist);
                    response.Status = StatusCode.OK;
                }
                else
                {
                    response.Status = StatusCode.Unauthorized;
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

        public RequestPasswordResponse ResetPasswordRequest(RequestPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
