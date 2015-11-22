namespace MVC.Services.Article
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Services.Article.Transfer;

	public interface IArticleService
	{
		GetArticleResponse GetArticle(int id, string languageCode);

		//Task<GetArticleResponse> GetArticleAsync(int id, string languageCode);

		//List<ArticleSummaryDto> GetArticleSummaries();

		//EditArticleResponse AddArticle(ArticleDto article);

		//Task<EditArticleResponse> AddArticleAsync(ArticleDto article);

		//EditArticleResponse UpdateArticle(ArticleDto article);

		//Task<EditArticleResponse> UpdateArticleAsync(ArticleDto article);

		//EditArticleResponse DeleteArticle(int id);

		//Task<EditArticleResponse> DeleteArticleAsync(int id);
	}
}
