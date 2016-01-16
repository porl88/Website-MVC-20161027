namespace MVC.Services.Culture
{
    using System.Threading.Tasks;
    using MVC.Services.Culture.Transfer;

    public interface ILanguageService
    {
        Task<GetLanguagesResponse> GetLanguagesAsync(GetLanguagesRequest request);
    }
}
