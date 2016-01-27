namespace MVC.Services.Account
{
    public enum CreateAccountStatus
    {
        Unknown,
        DuplicateEmail,
        DuplicateProviderUserKey,
        DuplicateUserName,
        InvalidPassword,
        InvalidEmail,
        InvalidAnswer,
        InvalidProviderUserKey,
        InvalidQuestion,
        InvalidUserName,
        ProviderError,
        Success,
        UserRejected
    }
}
