namespace MVC.Services.Account
{
    using System.Collections.Generic;
    using Core.Entities.Account;

    public interface IUserService
    {
        User GetUser(int userId);

        IEnumerable<User> GetUsers();

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userId);

        void SuspendUser(int userId);

		void UnlockUser(int userId);
    }
}
