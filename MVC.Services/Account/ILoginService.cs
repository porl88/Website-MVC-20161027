namespace MVC.Services.Account
{
    using Transfer;

    public interface ILoginService
	{
		LogInResponse LogIn(LogInRequest request);

		void LogOut();

		bool IsAuthenticated { get; }
	}
}
