namespace MVC.Services.Account.Transfer
{
    using System;

    public class ResetPasswordRequestRequest
    {
        public string UserName { get; set; }

        public TimeSpan Expires { get; set; }
    }
}
