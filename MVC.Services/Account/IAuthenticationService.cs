namespace MVC.Services.Account
{
    using Transfer;

    public interface IAuthenticationService
	{
        bool IsAuthenticated { get; }

        LoginResponse LogIn(LoginRequest request);

		void LogOut();

        ResetPasswordRequestResponse ResetPasswordRequest(ResetPasswordRequestRequest request);

        ResetPasswordResponse ResetPassword(ResetPasswordRequest request);
    }
}
