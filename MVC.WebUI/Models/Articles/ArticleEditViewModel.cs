namespace MVC.WebUI.Models.Article
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Article.Transfer;

    public class ArticleEditViewModel
    {
        public ArticleEditDto Article { get; set; }

        public List<SelectListItem> Languages { get; set; }
    }
}