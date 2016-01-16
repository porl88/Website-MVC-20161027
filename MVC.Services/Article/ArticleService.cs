﻿namespace MVC.Services.Article
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Data.EntityFramework;
    using Core.Entities.Article;
    using Core.Exceptions;
    using Transfer;

    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Article> articleRepository;
        private readonly IRepository<ArticleVersion> articleVersionRepository;
        private readonly IExceptionHandler exceptionHandler;

        public ArticleService(IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler)
        {
            this.unitOfWork = unitOfWork;
            this.articleRepository = unitOfWork.ArticleRepository;
            this.articleVersionRepository = unitOfWork.ArticleVersionRepository;
            this.exceptionHandler = exceptionHandler;
        }

        public async Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
        {
            var response = new GetArticlesResponse();

            try
            {
                response.Articles = await this.articleVersionRepository.GetAsync(q => q
                    .Where(x => x.LanguageId == request.LanguageId && x.IsPublished == true)
                    .OrderBy(x => x.Title)
                    .Select(x => new ArticleSummaryDto
                    {
                        Id = x.ArticleId,
                        Title = x.Title
                    })
                );

                response.Status = ResponseStatus.OK;
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
                response.Article = await this.articleVersionRepository.GetSingleAsync(
                    x => x.ArticleId == request.ArticleId && x.LanguageId == request.LanguageId && x.IsPublished,
                    x => new ArticleDto
                    {
                        Id = x.ArticleId,
                        Title = x.Title,
                        Content = x.Content,
                        LastUpdated = x.Updated
                    }
                );

                if (response.Article != null)
                {
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

        public async Task<GetArticlesResponse> GetEditArticlesAsync(GetArticlesRequest request)
        {
            var response = new GetArticlesResponse();

            try
            {
                response.Articles = await this.articleRepository.GetAsync(q => q
                    .SelectMany(a => a.ArticleVersions
                        .Where(l => l.LanguageId == request.LanguageId)
                        .DefaultIfEmpty(a.ArticleVersions.Where(x => x.LanguageId == 1).FirstOrDefault()
                    ),
                    (a, av) => new ArticleSummaryDto
                    {
                        Id = a.Id,
                        Title = av.Title
                    })
                 );

                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public async Task<EditArticleResponse> GetEditArticleAsync(GetArticleRequest request)
        {
            var response = new EditArticleResponse();

            try
            {
                response.Article = await this.articleVersionRepository.GetSingleAsync(
                    x => x.ArticleId == request.ArticleId && x.LanguageId == request.LanguageId,
                    x => new ArticleEditDto
                    {
                        ArticleId = x.ArticleId,
                        ArticleVersionId = x.Id,
                        LanguageId = x.LanguageId,
                        Title = x.Title,
                        Content = x.Content,
                        Publish = x.IsPublished
                    }
                );

                if (response.Article != null)
                {
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

        public async Task<EditArticleResponse> AddArticleAsync(ArticleEditDto article)
        {
            var response = new EditArticleResponse();

            try
            {
                var now = DateTimeOffset.Now;

                var newArticle = new Article
                {
                    Created = now,
                    Updated = now
                };

                if (article.Publish)
                {
                    newArticle.Published = now;
                }

                newArticle.ArticleVersions.Add(new ArticleVersion
                {
                    LanguageId = article.LanguageId,
                    Title = article.Title,
                    Content = article.Content,
                    IsPublished = article.Publish,
                    Created = now,
                    Updated = now
                });

                var add = this.articleRepository.Insert(newArticle);
                await this.unitOfWork.CommitAsync();

                var addVersion = add.ArticleVersions.First();

                response.Article = new ArticleEditDto
                {
                    ArticleId = add.Id,
                    ArticleVersionId = addVersion.Id,
                    Title = addVersion.Title,
                    Content = addVersion.Content
                };

                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
                response.Status = ResponseStatus.SystemError;
            }

            return response;
        }

        public async Task<EditArticleResponse> UpdateArticleAsync(ArticleEditDto article)
        {
            var response = new EditArticleResponse();

            try
            {
                var dbArticle = await this.articleRepository.GetAsync(article.ArticleId);
                if (dbArticle != null)
                {
                    var now = DateTimeOffset.Now;

                    dbArticle.Updated = now;

                    var dbArticleVersion = dbArticle.ArticleVersions.FirstOrDefault(x => x.Id == article.ArticleVersionId && x.LanguageId == article.LanguageId);

                    if (dbArticleVersion != null)
                    {
                        dbArticleVersion.Title = article.Title;
                        dbArticleVersion.Content = article.Content;
                        dbArticleVersion.IsPublished = article.Publish;
                        dbArticleVersion.Updated = now;
                    }
                    else
                    {
                        dbArticle.ArticleVersions.Add(new ArticleVersion
                        {
                            LanguageId = article.LanguageId,
                            Title = article.Title,
                            Content = article.Content,
                            IsPublished = article.Publish,
                            Created = now,
                            Updated = now
                        });
                    }

                    var update = this.articleRepository.Update(dbArticle);
                    await this.unitOfWork.CommitAsync();

                    var updatedVersion = update.ArticleVersions.First(x => x.Id == article.ArticleVersionId);
                    response.Article = new ArticleEditDto
                    {
                        Title = updatedVersion.Title
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

        public async Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
        {
            var response = new DeleteArticleResponse();

            try
            {
                var article = await this.articleRepository.GetAsync(request.ArticleId);
                if (article != null)
                {
                    this.articleRepository.Delete(article);
                    await this.unitOfWork.CommitAsync();
                    //response.Title = article.Title;
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
    }
}
