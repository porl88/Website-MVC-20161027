namespace MVC.WebUI.Modules
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Web;
	using Core.Configuration;

	// NB: Register in web.config System.WebServer/Modules
	public class SeoRedirectModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(this.OnBeginRequest);
		}

		public void Dispose()
		{
		}

		/*
		* RULES FOR SEO URL OPTIMISATION:
		* domain name must be consistent - e.g. must be either http://www.domain.com or http://domain.com but not both
		* casing should be consistent - e.g. always lower case
		* use of forward slash on the end should be consistent - i.e. must always have a forward slash on the end or always not have one
		* must use a PERMANENT redirect to indicate to the search engine that the requested URL should not be indexed
		* must not use more than one redirect
		* remove extension
		*/

		public void OnBeginRequest(object source, EventArgs e)
		{
			var request = HttpContext.Current.Request;
			var response = HttpContext.Current.Response;
			var absolutePath = request.Url.AbsolutePath;

			// do not redirect on posts - or static files such as images/css/js
			if (request.RequestType == "GET" && response.ContentType.Contains("text/html") && absolutePath != "/" && !Path.HasExtension(absolutePath))
			{
				var requestedUrl = request.Url.Host + absolutePath;
				var domain = request.IsLocal ? request.Url.Host : WebsiteConfig.WebsiteUrl;
				if (requestedUrl.EndsWith("/") || Regex.IsMatch(requestedUrl, @"[A-Z]") || request.Url.Host != domain)
				{
					// create corrected URL
					var correctedUrl = new UriBuilder
					{
						Scheme = request.Url.Scheme,
						Host = domain,
						Port = request.Url.Port,
						Path = absolutePath.ToLower().TrimEnd(new char[] { '/', ' ' }),
						Query = request.Url.Query.TrimStart(new char[] { '?' })
					};

					// do 301 redirect
					response.Clear();
					response.RedirectPermanent(correctedUrl.Uri.AbsoluteUri);
				}
			}
		}
	}
}
