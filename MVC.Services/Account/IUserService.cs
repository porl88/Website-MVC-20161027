namespace MVC.Services.Account
{
    using Transfer;

    public interface IUserService
    {
        CreateUserResponse CreateUser(CreateUserRequest request);

        //User GetUser(int userId);

        //IEnumerable<User> GetUsers();

        //      void UpdateUser(User user);

        //      void DeleteUser(int userId);

        //      void SuspendUser(int userId);

        //void UnlockUser(int userId);
    }
}
