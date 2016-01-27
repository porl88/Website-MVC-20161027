namespace MVC.Services
{
    using MVC.Core.Exceptions;

    public abstract class BaseResponse
	{
		public StatusCode Status { get; set; }

        public string Message { get; set; }
	}
}
