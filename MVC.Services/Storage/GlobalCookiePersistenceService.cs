namespace MVC.Services.Storage
{
    using System;
    using System.Web;

    public class GlobalCookiePersistenceService : IPersistenceService
    {
        private const string cookieName = "global";
        private readonly HttpContextBase context;

        public GlobalCookiePersistenceService(HttpContextBase context)
        {
            this.context = context;
        }

        public string GetValue(string key)
        {
            return this.GetCookie()?.Values.Get(key);
        }

        public void SaveValue(string key, string value)
        {
            var cookie = this.GetCookie();

            if (cookie == null)
            {
                cookie = this.CreateCookie();
            }

            cookie.Values.Remove(key);
            cookie.Values.Add(key, value);

            this.context.Response.Cookies.Add(cookie);
        }

        public void DeleteValue(string key)
        {
            this.GetCookie()?.Values.Remove(key);
        }

        public void DeleteAllValues()
        {
            if (this.GetCookie() != null)
            {
                var cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.context.Response.Cookies.Add(cookie);
            }
        }

        private HttpCookie GetCookie()
        {
            return this.context?.Request.Cookies[cookieName];
        }

        private HttpCookie CreateCookie()
        {
            var cookie = new HttpCookie(cookieName);
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddMonths(1);
            return cookie;
        }
    }
}
