namespace MVC.Services.Crawler
{
	using System.Threading.Tasks;
	using MVC.Services.Crawler.Transfer;

	public interface ICrawlerService
	{
		Task<CheckLinksResponse> CheckLinksAsync(CheckLinksRequest request);
	}
}
