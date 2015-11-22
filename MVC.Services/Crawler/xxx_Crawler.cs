namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
	using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

	public class PageLink
	{
		public Uri Page { get; set; }
		public Uri Link { get; set; }
		public int ErrorCode { get; set; }
		public string Message { get; set; }
		public string ContentType { get; set; }
	}

    public class Crawler
    {
        public List<PageLink> DownloadedLinks { get; private set; }

		private Queue<PageLink> UnprocessedPages { get; set; }

        private int Counter { get; set; }

        public Crawler()
        {
            this.DownloadedLinks = new List<PageLink>();
			this.UnprocessedPages = new Queue<PageLink>();
        }


		public async Task<List<PageLink>> CheckPageLinksAsync(string domain)
		{
			this.UnprocessedPages.Enqueue(new PageLink { Page = new Uri(domain), Link = new Uri(domain) });

			using (var client = new HttpClient())
			{
				// process all links on the page
				while (this.UnprocessedPages.Count > 0)
				{
					if (++this.Counter > 700) break;

					var link = this.UnprocessedPages.Dequeue();
					
                    try
					{
                        var request = new HttpRequestMessage
                        {
                            RequestUri = link.Link,
                            Method = this.CheckHeadersOnly(link.Link) ? HttpMethod.Head : HttpMethod.Get
                        };
                        
						var response = await client.SendAsync(request);
						var mimeType = response.Content.Headers.ContentType.MediaType;

						// add link headers to list of downloaded URLs - do not include variations on query string
						if (!this.DownloadedLinks.Any(x => (x.Link.AbsolutePath == link.Link.AbsolutePath && x.Link.Host == link.Link.Host)))
						{
							this.DownloadedLinks.Add(new PageLink
							{
								Page = link.Page,
								Link = link.Link,
								ErrorCode = (int)response.StatusCode,
								Message = response.ReasonPhrase,
								ContentType = mimeType
							});
						}

						if (response.IsSuccessStatusCode)
						{
							// add any links that contain content to the list of pages that have been successfully downloaded but not processed
							if ((mimeType == "text/html" || mimeType == "text/css") && (response.RequestMessage.RequestUri.Host.Replace("www.", string.Empty) == new Uri(domain).Host.Replace("www.","")))
							{
								var html = await response.Content.ReadAsStringAsync();
                                List<Uri> pageLinks = this.GetUrls(domain, html);
                                List<Uri> uncheckedLinks = pageLinks.Except(this.DownloadedLinks.Select(x => x.Link)).ToList();

                                foreach (var newLink in uncheckedLinks)
								{
									if (!this.UnprocessedPages.Any(x => x.Link.AbsolutePath == newLink.AbsolutePath))
									{
										this.UnprocessedPages.Enqueue(new PageLink
										{
											Page = link.Link,
											Link = newLink
										});
									}
								}
							}
						}
					}
					catch (WebException ex)
					{
						var response = (HttpWebResponse)ex.Response;
						this.DownloadedLinks.Add(new PageLink { Page = link.Page, Link = link.Link, ErrorCode = (int)response.StatusCode, Message = ex.Message, ContentType = response.ContentType });
					}
					catch (Exception ex)
					{
						this.DownloadedLinks.Add(new PageLink
						{
							ContentType = "Unknown",
							Link = link.Link,
							Page = link.Page,
							ErrorCode = 500,
							Message = ex.Message
						});
					}
				}
			}

			return this.DownloadedLinks;
		}



        private bool CheckHeadersOnly(Uri link)
        {
            switch (Path.GetExtension(link.AbsolutePath.ToLower()))
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".js":
                case ".mp4":
                case ".mp3":
                case ".webm":
                    return true;
            }

            return false;
        }



		//public async Task<List<PageLink>> CheckPageLinksAsync(string domain, string html)
		//{
		//	this.Counter++;
		//	if (this.Counter > 5) return this.DownloadedLinks;

		//	using (var client = new HttpClient())
		//	{
		//		var content = new StringBuilder();

		//		List<Uri> pageLinks = this.GetUrls(domain, html);
		//		//HttpContext.Current.Response.Write("pageLinks: " + pageLinks.Count + "<br />");

		//		List<Uri> uncheckedLinks = pageLinks.Except(this.DownloadedLinks.Select(x => x.Link)).ToList();
		//		//HttpContext.Current.Response.Write("uncheckedLinks: " + uncheckedLinks.Count + "<br />");
		//		var tasks = new List<Task<string>>();

		//		// process all links on the page
		//		foreach (var link in uncheckedLinks)
		//		{
		//			tasks.Add(xxx(domain, client, link));
		//		}

		//		var kkk = await Task.WhenAll(tasks);

		//		foreach (var z in kkk)
		//		{
		//			content.Append(z);
		//		}
                
		//		if (content.Length > 0)
		//		{
		//			await this.CheckPageLinksAsync(domain, content.ToString());
		//		}
		//	}

		//	return this.DownloadedLinks;
		//}

		//private async Task<string> xxx(string domain, HttpClient client, Uri link)
		//{
		//	try
		//	{
		//		// download link headers
		//		var request = new HttpRequestMessage
		//		{
		//			RequestUri = link,
		//			Method = Path.GetExtension(link.AbsolutePath) == ".jpg" ? HttpMethod.Head : HttpMethod.Get
		//		};
		//		//HttpContext.Current.Response.Write("<br />" + Path.GetExtension(link.AbsolutePath));
		//		var response = await client.SendAsync(request);




		//		//var response = await client.GetAsync(link.AbsoluteUri);
		//		var mimeType = response.Content.Headers.ContentType.MediaType;

		//		// add link headers to list of downloaded URLs - do not include variations on query string
		//		if (!this.DownloadedLinks.Any(x => (x.Link.AbsolutePath == link.AbsolutePath && x.Link.Host == link.Host)))
		//		{
		//			this.DownloadedLinks.Add(new PageLink
		//			{
		//				Page = link,
		//				Link = link,
		//				ErrorCode = (int)response.StatusCode,
		//				Message = response.ReasonPhrase,
		//				ContentType = mimeType
		//			});
		//		}

		//		if (response.IsSuccessStatusCode)
		//		{
		//			// add any links that contain content to the list of pages that have been successfully downloaded but not processed
		//			if ((mimeType == "text/html" || mimeType == "text/css") && (domain.Contains(link.Host.ToLower())))
		//			{
		//				return await response.Content.ReadAsStringAsync();
		//			}

		//			//this.DownloadFile(link, mimeType, response);
		//		}
		//	}
		//	catch (WebException ex)
		//	{
		//		var response = (HttpWebResponse)ex.Response;
		//		this.DownloadedLinks.Add(new PageLink { Page = link, Link = link, ErrorCode = (int)response.StatusCode, Message = ex.Message, ContentType = response.ContentType });
		//	}
		//	catch (Exception ex)
		//	{
		//		this.DownloadedLinks.Add(new PageLink
		//		{
		//			ContentType = "Unknown",
		//			Link = link,
		//			Page = link,
		//			ErrorCode = 500,
		//			Message = ex.Message
		//		});
		//	}
		//	return null;
		//}











        private async void DownloadFile(Uri link, string mimeType, HttpResponseMessage response)
        {
            string filePath = "/test" + link.AbsolutePath;

            switch (mimeType)
            {
                case "text/html":
                    if (filePath.EndsWith("/"))
                    {
                        filePath += "index.html";
                    }
                    else
                    {
                        filePath = Path.ChangeExtension(filePath, ".html");
                    }
                    break;
            }

			this.AutoCreateDirectories(filePath);

			using (var fileStream = File.Create(HttpContext.Current.Server.MapPath(filePath)))
			{
				await response.Content.CopyToAsync(fileStream);
			}
        }

		private void AutoCreateDirectories(string filePath)
		{
			string[] segments = filePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			for (var i = 0; i < segments.Length; i++)
			{
				var newPath = "/" + string.Join("/", segments.Take(i));
				var directoryPath = HttpContext.Current.Server.MapPath(newPath);
				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}
			}
		}

        //private Uri FormatLink(string link, Uri parentUri)
        //{
        //    if (link.StartsWith("//"))
        //    {
        //        link = "http:" + link;
        //    }
        //    else if (link.StartsWith("/"))
        //    {
        //        link = string.Format("{0}://{1}{2}", parentUri.Scheme, parentUri.Host, link);
        //    }

        //    Uri linkUri;
        //    if (Uri.TryCreate(link, UriKind.Absolute, out linkUri))
        //    {
        //        return linkUri;
        //    }

        //    return null;
        //}

        private List<Uri> GetUrls(string domain, string html)
        {
            string pattern = "(href|src)=\"(?!mailto:)([^\"]+)\"";

            var urls = Regex.Matches(html, pattern, RegexOptions.IgnoreCase).Cast<Match>().Select(x => x.Groups[2].Value.Trim().ToLower());

            var correctedUrls = new List<Uri>();
            foreach (string url in urls)
            {
                string newUrl;
				if (url.StartsWith("http"))
				{
					newUrl = url;
				}
				else if (url.StartsWith("/"))
				{
					//newUrl = "http://" + domain + url;
					newUrl = domain.TrimEnd(new char[] { '/'}) + url;
				}
				else
				{
					//newUrl = "http://" + domain + "/" + url;
					newUrl = domain.TrimEnd(new char[] { '/' }) + "/" + url;
				}

                Uri uri;
				if (Uri.TryCreate(newUrl, UriKind.Absolute, out uri))
                {
                    correctedUrls.Add(uri);
                }
                else
                {
                    //HttpContext.Current.Response.Write("<br />GetUrls Error: " + newUrl);
                }
            }

			return correctedUrls;
        }


/*
		public async Task<List<PageLink>> CheckPageLinksAsync(string domain, string html)
		{
			using (var client = new HttpClient())
			{
				var content = new StringBuilder();
				List<Uri> pageLinks = this.GetUrls(domain, html);

				List<Uri> uncheckedLinks = pageLinks.Except(this.DownloadedLinks.Select(x => x.Link)).ToList();

				// process all links on the page
				foreach (var link in uncheckedLinks)
				{
					try
					{
						// download link headers
						//var request = new HttpRequestMessage
						//{
						//    RequestUri = link,
						//    Method = Path.GetExtension(link.AbsolutePath) == ".jpg" ? HttpMethod.Head : HttpMethod.Get
						//};
						//HttpContext.Current.Response.Write("<br />" + Path.GetExtension(link.AbsolutePath));
						//var response = await client.SendAsync(request);

						var response = await client.GetAsync(link.AbsoluteUri);
						var mimeType = response.Content.Headers.ContentType.MediaType;

						// add link headers to list of downloaded URLs - do not include variations on query string
						if (!this.DownloadedLinks.Any(x => (x.Link.AbsolutePath == link.AbsolutePath && x.Link.Host == link.Host)))
						{
							this.DownloadedLinks.Add(new PageLink
							{
								Page = link,
								Link = link,
								ErrorCode = (int)response.StatusCode,
								Message = response.ReasonPhrase,
								ContentType = mimeType
							});
						}

						if (response.IsSuccessStatusCode)
						{
							// add any links that contain content to the list of pages that have been successfully downloaded but not processed
							if (mimeType == "text/html" || mimeType == "text/css")
							{
								content.Append(await response.Content.ReadAsStringAsync());
							}

							//this.DownloadFile(link, mimeType, response);
						}
					}
					catch (WebException ex)
					{
						var response = (HttpWebResponse)ex.Response;
						this.DownloadedLinks.Add(new PageLink { Page = link, Link = link, ErrorCode = (int)response.StatusCode, Message = ex.Message, ContentType = response.ContentType });
					}
					catch (Exception ex)
					{
						this.DownloadedLinks.Add(new PageLink
						{
							ContentType = "Unknown",
							Link = link,
							Page = link,
							ErrorCode = 500,
							Message = ex.Message
						});
					}
				}

				if (content.Length > 0)
				{
					await this.CheckPageLinksAsync(domain, content.ToString());
				}
			}

			return this.DownloadedLinks;
		}
*/
    }
}


