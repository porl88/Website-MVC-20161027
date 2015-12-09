namespace MVC.Services.Article
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public GetArticleResponse GetArticle(GetArticleRequest request)
        {
            var response = new GetArticleResponse();

            try
            {
                var article = this.articleVersionRepository.GetSingle(q => q.Where(x => x.ArticleId == request.ArticleId && x.Language.LanguageCode == request.LanguageCode).SingleOrDefault());
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

        public async Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request)
        {
            var response = new GetArticleResponse();

            try
            {
                var article = await this.articleVersionRepository.GetSingleAsync(q => q.Where(x => x.ArticleId == request.ArticleId && x.Language.LanguageCode == request.LanguageCode).SingleOrDefault());
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

        public GetArticlesResponse GetArticles(GetArticlesRequest request = null)
        {
            var response = new GetArticlesResponse();

            try
            {
                var articles = this.articleVersionRepository.Get();

                response.Articles = articles.Select(x => new ArticleSummaryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Description
                }).ToList();

                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public async Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request = null)
        {
            var response = new GetArticlesResponse();

            try
            {
                var articles = await this.articleVersionRepository.GetAsync();

                response.Articles = articles.Select(x => new ArticleSummaryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Description
                }).ToList();

                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public EditArticleResponse AddArticle(EditArticleRequest request)
        {
            var response = new EditArticleResponse();

            try
            {
                var now = DateTimeOffset.Now;
                var article = request.Article;

                var newArticle = new ArticleVersion
                {
                    Title = article.Title,
                    Content = article.Content,
                    Created = now,
                    Updated = now
                };

                var addedArticle = this.articleVersionRepository.Insert(newArticle);
                this.unitOfWork.Commit();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public async Task<EditArticleResponse> AddArticleAsync(EditArticleRequest request)
        {
            var response = new EditArticleResponse();

            try
            {
                var now = DateTimeOffset.Now;
                var article = request.Article;

                var newArticle = new ArticleVersion
                {
                    Title = article.Title,
                    Content = article.Content,
                    Created = now,
                    Updated = now
                };

                var addedArticle = this.articleVersionRepository.Insert(newArticle);
                await this.unitOfWork.CommitAsync();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public EditArticleResponse UpdateArticle(EditArticleRequest request)
        {
            var response = new EditArticleResponse();

            try
            {
                var article = request.Article;
                var dbArticle = this.articleVersionRepository.Get(article.ArticleVersionId);
                if (dbArticle != null)
                {
                    dbArticle.Title = article.Title;
                    dbArticle.Content = article.Content;
                    dbArticle.Updated = DateTime.Now;
                    var update = this.articleVersionRepository.Update(dbArticle);
                    this.unitOfWork.Commit();
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

        public async Task<EditArticleResponse> UpdateArticleAsync(EditArticleRequest request)
        {
            var response = new EditArticleResponse();

            try
            {
                var article = request.Article;
                var dbArticle = this.articleVersionRepository.Get(article.ArticleVersionId);
                if (dbArticle != null)
                {
                    dbArticle.Title = article.Title;
                    dbArticle.Content = article.Content;
                    dbArticle.Updated = DateTime.Now;
                    var update = this.articleVersionRepository.Update(dbArticle);
                    await this.unitOfWork.CommitAsync();
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

        //public EditArticleResponse DeleteArticle(int id)
        //{
        //    var response = new EditArticleResponse();

        //    try
        //    {
        //        var dbArticle = this.articleVersionRepository.Get(id);
        //        if (dbArticle != null)
        //        {
        //            this.articleVersionRepository.Delete(dbArticle);
        //            this.unitOfWork.Commit();
        //            response.Title = dbArticle.Title;
        //            response.Status = ResponseStatus.OK;
        //        }
        //        else
        //        {
        //            response.Status = ResponseStatus.NotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.exceptionHandler.HandleException(ex);
        //        response.Status = ResponseStatus.SystemError;
        //    }

        //    return response;
        //}

        //public async Task<EditArticleResponse> DeleteArticleAsync(int id)
        //{
        //    var response = new EditArticleResponse();

        //    try
        //    {
        //        var dbArticle = this.articleVersionRepository.Get(id);
        //        if (dbArticle != null)
        //        {
        //            this.articleVersionRepository.Delete(dbArticle);
        //            await this.unitOfWork.CommitAsync();
        //            response.Title = dbArticle.Title;
        //            response.Status = ResponseStatus.OK;
        //        }
        //        else
        //        {
        //            response.Status = ResponseStatus.NotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.exceptionHandler.HandleException(ex);
        //        response.Status = ResponseStatus.SystemError;
        //    }

        //    return response;
        //}
    }
}
