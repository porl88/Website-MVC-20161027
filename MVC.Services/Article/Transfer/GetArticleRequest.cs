namespace MVC.Services.Article.Transfer
{
    public class GetArticleRequest
    {
        public GetArticleRequest()
        {
            this.LanguageCode = "en-gb";
        }

        public int ArticleId { get; set; }

        public string LanguageCode { get; set; }
    }
}
