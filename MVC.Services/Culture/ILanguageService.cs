namespace MVC.Services.Culture
{
    using System.Threading.Tasks;
    using MVC.Services.Culture.Transfer;

    public interface ILanguageService
    {
        GetLanguagesResponse GetLanguages(GetLanguagesRequest request);

        Task<GetLanguagesResponse> GetLanguagesAsync(GetLanguagesRequest request);

        int GetPreferredLanguage();

        void SetPreferredLanguage(int languageId);
    }
}
