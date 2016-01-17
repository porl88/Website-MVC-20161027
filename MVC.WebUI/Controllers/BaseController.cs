namespace MVC.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Models.Shared;
    using Services.Culture;
    using Services.Culture.Transfer;

    public class BaseController : Controller
    {
        private readonly ILanguageService languageService;
        protected int LanguageId { get; private set; }

        public BaseController(ILanguageService languageService)
        {
            this.languageService = languageService;
            this.LanguageId = languageService.GetPreferredLanguage();
        }

        [ChildActionOnly]
        public PartialViewResult Languages()
        {
            var request = new GetLanguagesRequest
            {
                LanguageId = this.LanguageId
            };

            var response = this.languageService.GetLanguages(request);

            var model = new LanguageDropdownViewModel
            {
                LanguageId = this.LanguageId,
                Languages = response.Languages.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == this.LanguageId
                }).ToList()
            };

            return this.PartialView("~/views/shared/_languages.cshtml", model);
        }

        [ChildActionOnly, HttpPost]
        public PartialViewResult Languages(LanguageDropdownViewModel model)
        {
            var request = new GetLanguagesRequest
            {
                LanguageId = model.LanguageId
            };

            this.languageService.SetPreferredLanguage(model.LanguageId);

            var response = this.languageService.GetLanguages(request);

            model.Languages = response.Languages.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == model.LanguageId
            }).ToList();

            return this.PartialView("~/views/shared/_languages.cshtml", model);
        }
    }
}