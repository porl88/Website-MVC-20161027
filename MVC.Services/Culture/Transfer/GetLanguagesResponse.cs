namespace MVC.Services.Culture.Transfer
{
    using System.Collections.Generic;

    public class GetLanguagesResponse : BaseResponse
    {
        public List<LanguageDto> Languages { get; set; }
    }
}
