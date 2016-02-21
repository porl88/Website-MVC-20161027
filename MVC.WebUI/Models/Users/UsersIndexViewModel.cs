namespace MVC.WebUI.Models.Users
{
    using System.Collections.Generic;
    using Services.Account.Transfer;

    public class UsersIndexViewModel
    {
        public UsersIndexViewModel()
        {
            this.Users = new List<UserDto>();
        }

        public List<UserDto> Users { get; set; }
    }
}