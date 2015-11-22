namespace MVC.Services.Crawler
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Services.Crawler.Transfer;
	
	public interface yyy_ICrawlerService
    {
		Task<List<PageLinkDto>> CheckPageLinks();
    }
}
