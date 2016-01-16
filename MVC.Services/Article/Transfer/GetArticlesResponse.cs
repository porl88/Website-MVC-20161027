namespace MVC.Services.Article.Transfer
{
    using System.Collections.Generic;

    public class GetArticlesResponse : BaseResponse
    {
        public GetArticlesResponse ()
        {
            this.Articles = new List<ArticleSummaryDto>();
        }

        public List<ArticleSummaryDto> Articles { get; set; }
    }
}
