namespace MVC.Services.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Security; // in System.Web.ApplicationServices
    using Core.Exceptions;
    using Core.Entities.Account;
    using Transfer;
    using WebMatrix.WebData;

    /*

    nuget:

    install-package Microsoft.AspNet.WebPages.WebData


    in Global.asax:

    if (!WebSecurity.Initialized)
    {
        WebSecurity.InitializeDatabaseConnection("YOUR_DB_CONTEXT", "USER_TABLE", "ID_COLUMN", "USERNAME_COLUMN", true);
    }

    */

    public class SimpleMembershipAdapter : ILoginService, IAccountService
    {
        private readonly HttpContext context;
        private readonly IExceptionHandler exceptionHandler;

        public SimpleMembershipAdapter(IExceptionHandler exceptionHandler)
        {
            this.context = HttpContext.Current;
            this.exceptionHandler = exceptionHandler;
        }

        public bool IsAuthenticated
        {
            get
            {
                return WebSecurity.IsAuthenticated;
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

                if (WebSecurity.Login(request.UserName, request.Password, persist))
                {
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    // ???
                }

            }
            catch (MembershipCreateUserException ex)
            {
                // MembershipCreateStatus
                // https://msdn.microsoft.com/en-us/library/system.web.security.membershipcreateuserexception.statuscode(v=vs.110).aspx
                //e.
                //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.SystemError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        public void LogOut()
        {
            WebSecurity.Logout();
        }

        public bool ActivateAccount(string activateAccountToken)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
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
