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

        public CreateAccountResponse CreateAccount(CreateAccountRequest request)
        {
            var response = new CreateAccountResponse();

            try
            {
                var propertyValues = new
                {
                    Email = request.Email
                };

                response.ActivateAccountToken = WebSecurity.CreateUserAndAccount(request.UserName, request.Password, propertyValues, requireConfirmationToken: true);
                response.Status = StatusCode.OK;
            }
            // https://msdn.microsoft.com/en-us/library/system.web.security.membershipcreateuserexception.statuscode(v=vs.110).aspx
            catch (MembershipCreateUserException ex)
            {
                response.Status = StatusCode.BadRequest;
                response.Message = ex.ToString();
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
                response.Message = ex.ToString();
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
