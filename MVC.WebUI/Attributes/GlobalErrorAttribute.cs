namespace MVC.WebUI.Attributes
{
    using System.Web.Mvc;
    using Core.Exceptions;
    using Ninject;

    public class GlobalErrorAttribute : HandleErrorAttribute
	{
		[Inject]
		public IExceptionHandler exceptionHandler { get; set; }

		public override void OnException(ExceptionContext filterContext)
		{
			try
			{
				this.exceptionHandler.HandleException(filterContext.Exception);
			}
			catch
			{
			}
		}
	}
}