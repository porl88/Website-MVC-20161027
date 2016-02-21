namespace MVC.Services.Account.Transfer
{
    using System;

    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public TimeSpan? Persistence { get; set; }
    }
}
