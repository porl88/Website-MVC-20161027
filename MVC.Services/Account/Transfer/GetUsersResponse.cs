namespace MVC.Services.Account.Transfer
{
    using System.Collections.Generic;

    public class GetUsersResponse : BaseResponse
    {
        public GetUsersResponse()
        {
            this.Users = new List<UserDto>();
        }

        public List<UserDto> Users { get; set; }
    }
}
