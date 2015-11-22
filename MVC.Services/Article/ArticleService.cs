namespace MVC.Services.Article
{
    using System;
    using System.Linq;
    using Core.Data.EntityFramework;
    using Core.Entities.Article;
    using Core.Exceptions;
    using Services.Article.Transfer;

    public class ArticleService : IArticleService
	{
		private readonly IUnitOfWork unitOfWork;

		private readonly IRepository<ArticleVersion> articleVersionRepository;

		private readonly IExceptionHandler exceptionHandler;

		public ArticleService(IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler)
		{
			this.unitOfWork = unitOfWork;
			this.articleVersionRepository = unitOfWork.ArticleVersionRepository;
			this.exceptionHandler = exceptionHandler;
		}

		public GetArticleResponse GetArticle(int id, string languageCode = "en-gb")
		{
			var response = new GetArticleResponse();

			try
			{
				var article = this.articleVersionRepository.Get().SingleOrDefault(x => x.ArticleId == id && x.LanguageCode == languageCode);
				if (article != null)
				{
					response.Article = new ArticleDto
					{
						Title = article.Title,
						Content = article.Content
					};
					response.Status = ResponseStatus.OK;
				}
				else
				{
					response.Status = ResponseStatus.NotFound;
				}
			}
			catch (Exception ex)
			{
				this.exceptionHandler.HandleException(ex);
				response.Status = ResponseStatus.SystemError;
			}

			return response;
		}

		//public async Task<GetArticleResponse> GetArticleAsync(int id, string languageCode = "en-gb")
		//{
		//	var response = new GetArticleResponse();

		//	try
		//	{
		//		var article = await this.articleVersionRepository.Get().SingleOrDefault(x => x.ArticleId == id && x.LanguageCode == languageCode);
		//		if (article != null)
		//		{
		//			response.Article = new ArticleDto
		//			{
		//				Title = article.Title,
		//				Content = article.Content
		//			};
		//			response.Status = ResponseStatus.OK;
		//		}
		//		else
		//		{
		//			response.Status = ResponseStatus.NotFound;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public List<ArticleSummaryDto> GetArticleSummaries()
		//{
		//	var articles = this.articleVersionRepository.Get();

		//	return articles.Select(x => new ArticleSummaryDto
		//	{
		//		Id = x.Id,
		//		Title = x.Title,
		//		Summary = x.Summary
		//	}).ToList();
		//}

		//public EditArticleResponse AddArticle(ArticleDto article)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var now = DateTime.Now;

		//		var newArticle = new ArticleVersion
		//		{
		//			Title = article.Title,
		//			Content = article.Content,
		//			Created = now,
		//			Updated = now
		//		};

		//		var addedArticle = this.articleVersionRepository.Insert(newArticle);
		//		this.unitOfWork.Commit();
		//		response.Status = ResponseStatus.OK;
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public async Task<EditArticleResponse> AddArticleAsync(ArticleDto article)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var now = DateTime.Now;

		//		var newArticle = new ArticleVersion
		//		{
		//			Title = article.Title,
		//			Content = article.Content,
		//			Created = now,
		//			Updated = now
		//		};

		//		var addedArticle = this.articleVersionRepository.Insert(newArticle);
		//		await this.unitOfWork.CommitAsync();
		//		response.Status = ResponseStatus.OK;
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public EditArticleResponse UpdateArticle(ArticleDto article)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var dbArticle = this.articleVersionRepository.Get(article.Id);
		//		if (dbArticle != null)
		//		{
		//			dbArticle.Title = article.Title;
		//			dbArticle.Content = article.Content;
		//			dbArticle.Updated = DateTime.Now;
		//			var update = this.articleVersionRepository.Update(dbArticle);
		//			this.unitOfWork.Commit();
		//			response.Status = ResponseStatus.OK;
		//		}
		//		else
		//		{
		//			response.Status = ResponseStatus.NotFound;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public async Task<EditArticleResponse> UpdateArticleAsync(ArticleDto article)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var dbArticle = this.articleVersionRepository.Get(article.Id);
		//		if (dbArticle != null)
		//		{
		//			dbArticle.Title = article.Title;
		//			dbArticle.Content = article.Content;
		//			dbArticle.Updated = DateTime.Now;
		//			var update = this.articleVersionRepository.Update(dbArticle);
		//			await this.unitOfWork.CommitAsync();
		//			response.Status = ResponseStatus.OK;
		//		}
		//		else
		//		{
		//			response.Status = ResponseStatus.NotFound;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public EditArticleResponse DeleteArticle(int id)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var dbArticle = this.articleVersionRepository.Get(id);
		//		if (dbArticle != null)
		//		{
		//			this.articleVersionRepository.Delete(dbArticle);
		//			this.unitOfWork.Commit();
		//			response.Title = dbArticle.Title;
		//			response.Status = ResponseStatus.OK;
		//		}
		//		else
		//		{
		//			response.Status = ResponseStatus.NotFound;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}

		//public async Task<EditArticleResponse> DeleteArticleAsync(int id)
		//{
		//	var response = new EditArticleResponse();

		//	try
		//	{
		//		var dbArticle = this.articleVersionRepository.Get(id);
		//		if (dbArticle != null)
		//		{
		//			this.articleVersionRepository.Delete(dbArticle);
		//			await this.unitOfWork.CommitAsync();
		//			response.Title = dbArticle.Title;
		//			response.Status = ResponseStatus.OK;
		//		}
		//		else
		//		{
		//			response.Status = ResponseStatus.NotFound;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.exceptionHandler.HandleException(ex);
		//		response.Status = ResponseStatus.SystemError;
		//	}

		//	return response;
		//}
	}
}
