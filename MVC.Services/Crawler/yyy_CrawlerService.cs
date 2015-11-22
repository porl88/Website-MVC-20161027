namespace MVC.Services.Crawler
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Services.Crawler.Transfer;

	public class yyy_CrawlerService : yyy_ICrawlerService
	{
		private readonly Uri siteUri;

		private Queue<string> uncheckedValidLinks; // queue of absolute URIs

		private List<string> checkedLinks;

		private List<PageLinkDto> PageLinks;

		public yyy_CrawlerService(Uri siteUri)
		{
			this.siteUri = siteUri;
			this.uncheckedValidLinks = new Queue<string>();
			this.checkedLinks = new List<string>();
			this.PageLinks = new List<PageLinkDto>();
		}

		// TO DO:
		// Make sure use of Uri's is consistent
		// Capturing exceptions on Task.WhenAll()
		// Do not use AbsoluteUri as this converts to lower case - some servers are case sensitive

		public async Task<List<PageLinkDto>> CheckPageLinks()
		{
			// validate initial URL ??? - not needed if using Uri class???
			string currentPageLink;
			this.uncheckedValidLinks.Enqueue(this.siteUri.AbsoluteUri);

			using (var client = new HttpClient())
			{
				do
				{
					// get next unchecked link from queue
					currentPageLink = this.uncheckedValidLinks.Dequeue();

					// get the HTML from the URL
					var pageHtml = await client.GetStringAsync(currentPageLink);

					// add current url to checked list
					checkedLinks.Add(currentPageLink);

					// get all the unique link URLs that are in the HTML
					var pageLinks = this.GetPageLinks(pageHtml);

					// remove links already checked
					pageLinks = pageLinks.Except(checkedLinks).ToList();

					if (pageLinks.Count == 0)
					{
						continue;
					}

					// get response headers to check if links are valid
					var tasks = new List<Task<HttpResponseMessage>>();

					foreach (var link in pageLinks)
					{
						try
						{
							tasks.Add(client.SendAsync(new HttpRequestMessage
							{
								Method = HttpMethod.Head,
								RequestUri = new Uri(link)
							}));
						}
						catch (Exception ex)
						{
							this.PageLinks.Add(new PageLinkDto
							{
								Link = new Uri(link),
								PageUrl = new Uri(currentPageLink),
								StatusMessage = ex.Message
							});
						}
					}

					var linkResponses = Task.WhenAll(tasks);

					try
					{
						await linkResponses;
						tasks.ForEach(t => this.AddLinkResponsesToLists(currentPageLink, t.Result));
					}
					catch (Exception)
					{
						linkResponses.Exception.Handle(exception =>
						{
							this.PageLinks.Add(new PageLinkDto
						   {
							   //Link = response.RequestMessage.RequestUri,
							   //PageUrl = new Uri(currentPageLink),
							   StatusCode = 500,
							   StatusMessage = string.Format("Exception: {1}", exception.Message)
						   });
							return true;
						});
					}

					// now that all pageLinks have been checked, add to list of checked links
					checkedLinks = checkedLinks.Union(pageLinks).ToList();
				}
				while (this.uncheckedValidLinks.Count > 0);
			}

			return this.PageLinks;
		}

		private void AddLinkResponsesToLists(string currentPageLink, HttpResponseMessage response)
		{
			try
			{
				var requestedUri = response.RequestMessage.RequestUri;
				var contentType = response.Content.Headers.ContentType.MediaType;

				// add pages that contain HTML (excluding pages for external sites) to the list of pages that need their links checked
				if (response.IsSuccessStatusCode && contentType == "text/html" && requestedUri.Host.Equals(this.siteUri.Host))
				{
					this.uncheckedValidLinks.Enqueue(requestedUri.AbsoluteUri);
				}

				this.PageLinks.Add(new PageLinkDto
				{
					Link = response.RequestMessage.RequestUri,
					PageUrl = new Uri(currentPageLink),
					StatusCode = (int)response.StatusCode,
					StatusMessage = response.ReasonPhrase,
					ContentType = contentType
				});
			}
			catch (Exception ex)
			{
				this.PageLinks.Add(new PageLinkDto
				{
					Link = response.RequestMessage.RequestUri,
					PageUrl = new Uri(currentPageLink),
					StatusCode = (int)response.StatusCode,
					StatusMessage = string.Format("Response StatusText: {0}, Exception: {1}", response.ReasonPhrase, ex.Message),
				});
			}
		}

		private List<string> GetPageLinks(string pageHtml)
		{
			var pageLinks = new List<string>();
			var pattern = "(href|src)=\"(.*?)\"";
			MatchCollection matches = Regex.Matches(pageHtml, pattern, RegexOptions.IgnoreCase);

			foreach (var match in matches.Cast<Match>())
			{
				var link = match.Groups[2].Value;

				if (!string.IsNullOrWhiteSpace(link))
				{
					Uri uri;
					if (Uri.TryCreate(this.siteUri, link, out uri) || Uri.TryCreate(link, UriKind.Absolute, out uri))
					{
						if (!pageLinks.Any(x => x == uri.AbsoluteUri))
						{
							pageLinks.Add(uri.AbsoluteUri);
						}
					}
					else
					{
						this.PageLinks.Add(new PageLinkDto
						{
							StatusCode = 400,
							StatusMessage = string.Format("Link is not a well-formed Uri: " + link),
						});
					}
				}
			}

			return pageLinks;
		}
	}
}
