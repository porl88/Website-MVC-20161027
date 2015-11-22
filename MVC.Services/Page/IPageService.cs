namespace MVC.Services.Page
{
    using System.Threading.Tasks;
    using Transfer;

    public interface IPageService
    {
        GetPageResponse GetPage(int pageId, string languageCode = "en-gb");

        Task<GetPageResponse> GetPageAsync(int id, string languageCode = "en-gb");
    }
}
