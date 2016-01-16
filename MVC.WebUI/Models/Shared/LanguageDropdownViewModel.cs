namespace MVC.WebUI.Models.Shared
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class LanguageDropdownViewModel
    {
        public LanguageDropdownViewModel()
        {
            this.Languages = new List<SelectListItem>();
        }

        public int LanguageId { get; set; }

        public List<SelectListItem> Languages { get; set; }
    }
}