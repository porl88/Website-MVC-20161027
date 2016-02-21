namespace MVC.Services.Account
{
    using Transfer;

    public interface IAccountService
	{
        CreateAccountResponse CreateAccount(CreateAccountRequest request);

        ActivateAccountResponse ActivateAccount(ActivateAccountRequest request);

		DeleteAccountResponse DeleteAccount(DeleteAccountRequest request);

		ChangePasswordResponse ChangePassword(ChangePasswordRequest request);

        // UpdateAccount???
        // GetAccountActivationToken???
	}
}
