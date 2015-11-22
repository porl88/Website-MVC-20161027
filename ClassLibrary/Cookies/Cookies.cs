namespace ClassLibrary.Cookies
{
	using System;
	using System.Web;

	public class Cookies
	{
		private readonly HttpContextBase context;

		public Cookies(HttpContextBase context)
		{
			this.context = context;
		}

		public void DeleteCookie(string cookieName)
		{
			if (this.context != null && this.context.Request.Cookies[cookieName] != null)
			{
				var cookie = new HttpCookie(cookieName);
				cookie.Expires = DateTime.Now.AddDays(-1);
				this.context.Response.Cookies.Add(cookie);
			}
		}
	}
}
