namespace MVC.Services.Account
{
    using System.Collections.Generic;
    using Core.Entities.Account;

    public interface IUserService
    {
        User GetUser(int userId);

        IEnumerable<User> GetUsers();

        void CreateUser(User user); /* 
        overlaps with AuthenticationService.CreateAccount, but that is OK
        auto generates password if none is supplied - or perhaps no passowrd
                                        doesn't make sense for user to have password!
                                        either generate accountConfimation token - or auto-confirm and send password reset token*/

        void UpdateUser(User user);

        void DeleteUser(int userId);

        void SuspendUser(int userId);

		void UnlockUser(int userId);
    }
}
