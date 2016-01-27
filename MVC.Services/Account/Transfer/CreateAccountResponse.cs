namespace MVC.Services.Account.Transfer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateAccountResponse : BaseResponse
    {
        public string ActivateAccountToken { get; set; }

        public CreateAccountStatus CreateAccountStatus { get; set; }
    }
}
