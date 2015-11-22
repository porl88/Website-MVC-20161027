namespace MVC.Services.Account
{
	using System;

	public interface ILoginService
	{
		bool LogIn(string userName, string password);

		bool LogIn(string userName, string password, TimeSpan persistence);

		void LogOut();

		bool IsAuthenticated { get; }
	}
}
