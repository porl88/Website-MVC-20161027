namespace MVC.WebUI.Errors
{
    using Core.Exceptions;
    using Services.Account;

    public static class ErrorMessageHelper
    {
        public static string GetErrorMessage(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.InternalServerError:
                    return "<p>An error has occurred on the server. This might be a temporary issue. Please verify your entry and try again.</p><p>If the problem persists, <a href=\"/contact/technical\">click here to contact technical support</a>.</p>";
                default:
                    return "<p>An unknown error has occurred. Please verify your entry and try again.</p><p>If the problem persists, please contact your system administrator.</p>";
            }
        }

        public static string GetErrorMessage(CreateAccountStatus status)
        {
            switch (status)
            {
                case CreateAccountStatus.DuplicateUserName:
                    return "<p>The username already exists. Please enter a different username.</p>";
                case CreateAccountStatus.DuplicateEmail:
                    return "<p>A username for that email address already exists. Please enter a different email address.</p>";
                case CreateAccountStatus.InvalidUserName:
                    return "<p>The user name provided is invalid. Please check the value and try again.</p>";
                case CreateAccountStatus.InvalidPassword:
                    return "<p>The password provided is invalid. Please enter a valid password value.</p>";
                case CreateAccountStatus.InvalidEmail:
                    return "<p>The email address provided is invalid. Please check the value and try again.</p>";
                case CreateAccountStatus.InvalidAnswer:
                    return "<p>The password retrieval answer provided is invalid. Please check the value and try again.</p>";
                case CreateAccountStatus.InvalidQuestion:
                    return "<p>The password retrieval question provided is invalid. Please check the value and try again.</p>";
                case CreateAccountStatus.ProviderError:
                    return "<p>The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.</p>";
                case CreateAccountStatus.UserRejected:
                    return "<p>The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.</p>";
                default:
                    return "<p>An unknown error has occurred. Please verify your entry and try again.</p><p>If the problem persists, please contact your system administrator.</p>";
            }
        }
    }
}