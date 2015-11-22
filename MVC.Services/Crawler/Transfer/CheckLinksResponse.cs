namespace MVC.Services.Crawler.Transfer
{
	using System.Collections.Generic;

	public class CheckLinksResponse : BaseResponse
	{
		public CheckLinksResponse()
		{
			Links = new List<PageLinkDto>();
		}

		public List<PageLinkDto> Links { get; set; }
	}
}
