namespace MVC.Services.Article
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Services.Article.Transfer;

	public interface IArticleService
	{
		GetArticleResponse GetArticle(GetArticleRequest request);

        Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request);

        GetArticlesResponse GetArticles(GetArticlesRequest request = null);

        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request = null);

        EditArticleResponse AddArticle(EditArticleRequest request);

        Task<EditArticleResponse> AddArticleAsync(EditArticleRequest request);

        EditArticleResponse UpdateArticle(EditArticleRequest request);

        Task<EditArticleResponse> UpdateArticleAsync(EditArticleRequest request);

        EditArticleResponse DeleteArticle(DeleteArticleRequest request);

        Task<EditArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);
    }
}
