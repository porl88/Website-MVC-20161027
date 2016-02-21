namespace MVC.Services.Account
{
    using MVC.Services.Account.Transfer;

    public interface IUserService
    {
        CreateUserResponse CreateUser(CreateUserRequest request);

        // GetUserResponse GetUser(GetUserRequest request) - userId, userName, email

        GetUsersResponse GetUsers(GetUsersRequest request);

        // EditUserResponse UpdateUser(EditUserRequest request);

        // DeleteUserResponse DeleteUser(DeleteUserRequest request);

        // SuspendUserResponse SuspendUser(SuspendUserRequest request);

        // UnlockUserResponse UnlockUser(UnlockUserRequest request);

        // GetAccountActivationToken???
    }
}
