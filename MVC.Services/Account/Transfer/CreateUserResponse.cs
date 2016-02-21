namespace MVC.Services.Account.Transfer
{
    using MVC.Services.Account;

    public class CreateUserResponse : BaseResponse
    {
        public CreateAccountStatus CreateAccountStatus { get; set; }

        public string Password { get; set; }
    }
}
