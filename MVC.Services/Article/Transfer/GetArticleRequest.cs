namespace MVC.Services.Article.Transfer
{
    public class GetArticleRequest
    {
        public GetArticleRequest()
        {
            this.LanguageId = 1;
        }

        public int ArticleId { get; set; }

        public int LanguageId { get; set; }
    }
}

