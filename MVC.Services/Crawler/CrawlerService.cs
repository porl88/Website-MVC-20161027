namespace MVC.Services.Crawler
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ClassLibrary.WebRequest;
    using Core.Exceptions;
    using MVC.Services.Crawler.Transfer;

    public class CrawlerService : ICrawlerService
	{
		private class UnprocessedLink
		{
			public Uri PageUrl { get; set; }

			public Uri Link { get; set; }
		}

		private CheckLinksResponse response;

		private ConcurrentQueue<UnprocessedLink> unprocessedLinks;

		private List<Uri> processedLinks;

		private Uri baseUri;

		public async Task<CheckLinksResponse> CheckLinksAsync(CheckLinksRequest request)
		{
			this.response = new CheckLinksResponse();
			unprocessedLinks = new ConcurrentQueue<UnprocessedLink>();
			processedLinks = new List<Uri>();

			if (request.Domain != null)
			{
				this.baseUri = request.Domain;
				this.unprocessedLinks.Enqueue(new UnprocessedLink
				{
					PageUrl = request.Domain,
					Link = request.Domain
				});

				while (this.unprocessedLinks.Count > 0)
				{
					this.response.Links.AddRange(await this.ProcessLinksAsync());
				}
			}
			else
			{
				this.response.Status = StatusCode.BadRequest;
			}

			return this.response;
		}

		private async Task<List<PageLinkDto>> ProcessLinksAsync()
		{
			var links = new List<PageLinkDto>();

			if (this.unprocessedLinks.Count > 0)
			{
				var batchWebRequest = new WebRequest<UnprocessedLink, PageLinkDto>();
				batchWebRequest.OnWebRequest += BatchWebRequest_OnWebRequest;
				links.AddRange(await batchWebRequest.RunBatchWebRequestAsync(this.unprocessedLinks));
			}

			return links;
		}

		private async Task<PageLinkDto> BatchWebRequest_OnWebRequest(object sender, WebRequestEventArgs<UnprocessedLink> e)
		{
			var response = new PageLinkDto
			{
				PageUrl = e.Request.PageUrl,
				Link = e.Request.Link
			};

			var url = e.Request.Link.AbsoluteUri;

			this.processedLinks.Add(response.Link);

			try
			{
				using (var httpResponse = await e.Client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url)))
				{
					response.StatusCode = (int)httpResponse.StatusCode;
					response.StatusMessage = httpResponse.ReasonPhrase;
					response.ContentType = httpResponse.Content.Headers.ContentType.MediaType;

					if (httpResponse.IsSuccessStatusCode && response.ContentType == "text/html" && e.Request.Link.Host == this.baseUri.Host)
					{
						var pageContent = await e.Client.GetStringAsync(url);
						var pageLinks = this.GetPageLinksFromHtml(pageContent).Distinct().ToList();
						this.ProcessLinks(pageLinks, e.Request.Link);
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 500;
				response.StatusMessage = ex.Message;
			}

			return response;
		}

		private List<string> GetPageLinksFromHtml(string pageHtml)
		{
			var pattern = "(href|src)=\"(.*?)\"";
			var matches = Regex.Matches(pageHtml, pattern, RegexOptions.IgnoreCase);
			return matches.Cast<Match>().Select(x => x.Groups[2].Value.Trim(' ', '/')).ToList();
		}

		private void ProcessLinks(List<string> links, Uri pageUri)
		{
			foreach (var link in links)
			{
				if (!string.IsNullOrWhiteSpace(link))
				{
					Uri uri;
					if (Uri.TryCreate(this.baseUri, link, out uri) || Uri.TryCreate(link, UriKind.Absolute, out uri))
					{
						var processedUrl = uri.Host + uri.AbsolutePath;
						if (!this.processedLinks.Any(x => x.Host + x.AbsolutePath == processedUrl) && !this.unprocessedLinks.Any(x => x.Link.Host + x.Link.AbsolutePath == processedUrl))
						{
							if (uri.Scheme == "http" || uri.Scheme == "https")
							{
								this.unprocessedLinks.Enqueue(new UnprocessedLink
								{
									PageUrl = pageUri,
									Link = uri
								});
							}
						}
					}
					else
					{
						this.response.Links.Add(new PageLinkDto
						{
							PageUrl = pageUri,
							StatusCode = 400,
							StatusMessage = string.Format("Link is not a well-formed Uri: " + link)
						});
						//this.processedLinks.Add() ?????
					}
				}
				else
				{
					this.response.Links.Add(new PageLinkDto
					{
						PageUrl = pageUri,
						StatusCode = 400,
						StatusMessage = string.Format("The page has one or more empty href or src attributes.")
					});
				}
			}
		}
	}
}
