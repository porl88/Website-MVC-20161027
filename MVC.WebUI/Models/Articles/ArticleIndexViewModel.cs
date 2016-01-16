namespace MVC.WebUI.Models.Article
{
    using System.Collections.Generic;
    using Services.Article.Transfer;

    public class ArticleIndexViewModel
    {
        public List<ArticleSummaryDto> Articles { get; set; }
    }
}