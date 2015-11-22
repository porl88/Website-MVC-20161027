namespace MVC.Services.Crawler.Transfer
{
    using System;

    public class PageLinkDto
    {
        public Uri PageUrl { get; set; }

        public Uri Link { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string ContentType { get; set; }
    }
}
