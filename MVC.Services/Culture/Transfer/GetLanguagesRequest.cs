namespace MVC.Services.Culture.Transfer
{
    public class GetLanguagesRequest
    {
        public GetLanguagesRequest()
        {
            this.LanguageId = 1;
        }

        public int LanguageId { get; set; }
    }
}
