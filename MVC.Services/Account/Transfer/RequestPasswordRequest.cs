namespace MVC.Services.Account.Transfer
{
    using System;

    public class RequestPasswordRequest
    {
        public string UserName { get; set; }

        public TimeSpan Expires { get; set; }
    }
}
