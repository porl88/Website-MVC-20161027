namespace MVC.Services.Account
{
    using Transfer;

    public interface IAuthenticationService
	{
		LogInResponse LogIn(LogInRequest request);

		void LogOut();

		bool IsAuthenticated { get; }
	}
}
