namespace MVC.Services.Account
{
    using System;
    using System.Threading.Tasks;
    using Core.Entities.Account;
    using Transfer;

    public interface IAccountService
	{
        CreateAccountResponse CreateAccount(CreateAccountRequest request);

        bool ActivateAccount(string activateAccountToken);

		void DeleteAccount(string userName);

		bool ChangePassword(string oldPassword, string newPassword);

		string ResetPasswordRequest(string userName, TimeSpan expires); // creates token - should be in IAuthenticationService???

		bool ResetPassword(string resetPasswordToken, string newPassword);

        User GetUser(string userName); // ??? 
	}
}
