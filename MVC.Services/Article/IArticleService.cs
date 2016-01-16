namespace MVC.Services.Article
{
    using System.Threading.Tasks;
    using Article.Transfer;

    public interface IArticleService
	{
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request);

        Task<GetArticlesResponse> GetEditArticlesAsync(GetArticlesRequest request);

        Task<EditArticleResponse> GetEditArticleAsync(GetArticleRequest request);

        Task<EditArticleResponse> AddArticleAsync(ArticleEditDto article);

        Task<EditArticleResponse> UpdateArticleAsync(ArticleEditDto article);

        Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);
    }
}
