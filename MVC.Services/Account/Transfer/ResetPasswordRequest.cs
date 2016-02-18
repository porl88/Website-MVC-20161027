namespace MVC.Services.Account.Transfer
{
    public class ResetPasswordRequest
    {
        public string ResetPasswordToken { get; set; }

        public string NewPassword { get; set; }
    }
}
