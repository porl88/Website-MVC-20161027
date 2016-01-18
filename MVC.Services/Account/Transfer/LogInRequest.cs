using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services.Account.Transfer
{
    public class LogInRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public TimeSpan Persistence { get; set; }
    }
}
