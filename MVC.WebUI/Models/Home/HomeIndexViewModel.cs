namespace MVC.WebUI.Models.Home
{
    using System.Collections.Generic;
    using Services.Culture.Transfer;
    using Services.Page.Transfer;

    public class HomeIndexViewModel
    {
        public PageDto Page { get; set; }

        public List<LanguageDto> Languages { get; set; }
    }
}