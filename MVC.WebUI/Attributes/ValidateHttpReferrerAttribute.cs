namespace MVC.WebUI.Attributes
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    [Obsolete("Not needed since Url.IsLocalUrl() was introduced.")]
	public class ValidateHttpReferrerAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext != null)
			{
				var httpRequest = filterContext.HttpContext.Request;

				if (httpRequest.UrlReferrer == null)
				{
					throw new HttpException("Invalid submission");
				}

				if (!httpRequest.UrlReferrer.Host.Equals(httpRequest.Url.Host, StringComparison.InvariantCultureIgnoreCase))
				{
					throw new HttpException("This form wasn't submitted from this site!");
				}
			}
		}
	}
}