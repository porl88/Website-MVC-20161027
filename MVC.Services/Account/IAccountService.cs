namespace MVC.Services.Account
{
	using System;
    using Core.Entities.Account;

	public interface IAccountService
	{
		string CreateAccount(string email, string password);

		string CreateAccount(string userName, string password, string email);

		bool ActivateAccount(string activateAccountToken);

		void DeleteAccount(string userName);

		bool ChangePassword(string oldPassword, string newPassword);

		string ResetPasswordRequest(string userName, TimeSpan expires); // creates token

		bool ResetPassword(string resetPasswordToken, string newPassword);

        User GetUser(string userName);
	}
}
