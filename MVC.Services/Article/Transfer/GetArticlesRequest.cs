namespace MVC.Services.Article.Transfer
{
    public class GetArticlesRequest
    {
        public GetArticlesRequest()
        {
            this.LanguageId = 1;
        }

        public int LanguageId { get; set; }
    }
}
