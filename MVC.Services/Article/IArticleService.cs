namespace MVC.Services.Article
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Services.Article.Transfer;

	public interface IArticleService
	{
		GetArticleResponse GetArticle(GetArticleRequest request);

        Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request);

        List<ArticleSummaryDto> GetArticles();

        Task<List<ArticleSummaryDto>> GetArticlesAsync();

        EditArticleResponse AddArticle(ArticleDto article);

        Task<EditArticleResponse> AddArticleAsync(ArticleDto article);

        EditArticleResponse UpdateArticle(EditArticleRequest request);

        Task<EditArticleResponse> UpdateArticleAsync(EditArticleRequest request);

        //EditArticleResponse DeleteArticle(int id);

        //Task<EditArticleResponse> DeleteArticleAsync(int id);
    }
}
