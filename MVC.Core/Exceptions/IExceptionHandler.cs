namespace MVC.Core.Exceptions
{
	using System;

	public interface IExceptionHandler
	{
		void HandleException(Exception ex);
	}
}
