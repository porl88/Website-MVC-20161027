namespace MVC.Services.Article.Transfer
{
    public class DeleteArticleRequest
    {
        public DeleteArticleRequest()
        {
            this.LanguageId = 1;
        }

        public int ArticleId { get; set; }

        public int LanguageId { get; set; }
    }
}
