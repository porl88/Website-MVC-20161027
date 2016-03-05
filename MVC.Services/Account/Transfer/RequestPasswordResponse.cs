namespace MVC.Services.Account.Transfer
{
    public class RequestPasswordResponse : BaseResponse
    {
        public string ResetPasswordToken { get; set; }
    }
}
