namespace MVC.Services.Account
{
    using System;
    using Transfer;

    public interface ILoginService
	{
		bool LogIn(LogInRequest request);

		void LogOut();

		bool IsAuthenticated { get; }
	}
}
