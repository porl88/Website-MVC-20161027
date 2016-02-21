namespace MVC.Services.Account.Transfer
{
    public class CreateUserRequest
    {
        public UserDto User { get; set; }

        public string Password { get; set; }
    }
}
