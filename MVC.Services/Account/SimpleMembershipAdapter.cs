namespace MVC.Services.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security; // in System.Web.ApplicationServices
    using Core.Exceptions;
    using Transfer;
    using WebMatrix.WebData;

    /***********************************************************************************************************************

    nuget:

    install-package Microsoft.AspNet.WebPages.WebData


    in Global.asax:

    if (!WebSecurity.Initialized)
    {
        WebSecurity.InitializeDatabaseConnection("YOUR_DB_CONTEXT", "USER_TABLE", "ID_COLUMN", "USERNAME_COLUMN", true);
    }

    *************************************************************************************************************************/

    public class SimpleMembershipAdapter : IAuthenticationService, IAccountService, IUserService
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

        public LoginResponse LogIn(LoginRequest request)
        {
            var response = new LoginResponse();

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

        public ResetPasswordRequestResponse ResetPasswordRequest(ResetPasswordRequestRequest request)
        {
            var response = new ResetPasswordRequestResponse();

            try
            {
                if (WebSecurity.UserExists(request.UserName))
                {
                    response.ResetPasswordToken = WebSecurity.GeneratePasswordResetToken(request.UserName, (int)request.Expires.TotalMinutes);
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

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            var response = new ResetPasswordResponse();

            try
            {
                if (WebSecurity.ResetPassword(request.ResetPasswordToken, request.NewPassword))
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

        public ActivateAccountResponse ActivateAccount(ActivateAccountRequest request)
        {
            var response = new ActivateAccountResponse();

            try
            {
                if (WebSecurity.ConfirmAccount(request.ActivateAccountToken))
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

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            var response = new ChangePasswordResponse();

            try
            {
                if (WebSecurity.ChangePassword(request.UserName, request.OldPassword, request.NewPassword))
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
                response.CreateAccountStatus = this.MapCreateAccountStatus(ex.StatusCode);
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest request)
        {
            var response = new DeleteAccountResponse();

            try
            {
                var userName = request.UserName;
                var roles = Roles.GetRolesForUser(userName);
                if (roles.Any())
                {
                    Roles.RemoveUserFromRoles(userName, roles);
                }

                var membership = (SimpleMembershipProvider)Membership.Provider;
                membership.DeleteAccount(userName); // deletes record from webpages_Membership table
                membership.DeleteUser(userName, true); // deletes record from UserProfile table
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            try
            {
                var user = request.User;
                var password = Membership.GeneratePassword(14, 5);

                var propertyValues = new
                {
                    Email = user.Email
                };

                WebSecurity.CreateUserAndAccount(user.UserName, password, propertyValues, requireConfirmationToken: false);
                response.Status = StatusCode.OK;
            }
            // https://msdn.microsoft.com/en-us/library/system.web.security.membershipcreateuserexception.statuscode(v=vs.110).aspx
            catch (MembershipCreateUserException ex)
            {
                response.Status = StatusCode.BadRequest;
                response.CreateAccountStatus = this.MapCreateAccountStatus(ex.StatusCode);
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.InternalServerError;
                this.exceptionHandler.HandleException(ex);
            }

            return response;
        }

        private CreateAccountStatus MapCreateAccountStatus(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return CreateAccountStatus.DuplicateUserName;
                case MembershipCreateStatus.DuplicateEmail:
                    return CreateAccountStatus.DuplicateEmail;
                case MembershipCreateStatus.InvalidUserName:
                    return CreateAccountStatus.InvalidUserName;
                case MembershipCreateStatus.InvalidEmail:
                    return CreateAccountStatus.InvalidEmail;
                case MembershipCreateStatus.InvalidPassword:
                    return CreateAccountStatus.InvalidPassword;
                case MembershipCreateStatus.InvalidAnswer:
                    return CreateAccountStatus.InvalidAnswer;
                case MembershipCreateStatus.InvalidQuestion:
                    return CreateAccountStatus.InvalidQuestion;
                case MembershipCreateStatus.ProviderError:
                    return CreateAccountStatus.ProviderError;
                case MembershipCreateStatus.UserRejected:
                    return CreateAccountStatus.UserRejected;
                default:
                    return CreateAccountStatus.Unknown;
            }
        }
    }
}
