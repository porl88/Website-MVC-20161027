namespace MVC.WebUI.Errors
{
    using Core.Exceptions;

    public static class ErrorMessageHelper
    {
        public static string GetErrorMessage(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.InternalServerError:
                    return "An error has occurred on the server. This might be a temporary issue. Please try again. If the problem persists, click here to contact technical support.";
                default:
                    return "An unknown error has occurred.";
            }
        }
    }
}