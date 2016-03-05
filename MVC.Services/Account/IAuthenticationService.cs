namespace MVC.Services.Account
{
    using MVC.Services.Account.Transfer;

    public interface IAuthenticationService
    {
        LoginResponse LogIn(LoginRequest request);

        void LogOut();

        bool IsAuthenticated { get; }

        int CurrentUserId { get; }

        string CurrentUserName { get; }

        ResetPasswordRequestResponse ResetPasswordRequest(RequestPasswordRequest request);

        ResetPasswordResponse ResetPassword(ResetPasswordRequest request);
    }
}
