namespace MVC.Services.Page
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Data.EntityFramework;
    using Core.Exceptions;
    using Core.Entities.Website;
    using Transfer;
    using System.Text.RegularExpressions;
    using ClassLibrary.Validation;
    using System.Data.Entity;

    public class PageService : IPageService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepository<PageVersion> pageVersionRepository;

        private readonly IExceptionHandler exceptionHandler;

        public PageService(IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler)
        {
            this.unitOfWork = unitOfWork;
            this.pageVersionRepository = unitOfWork.PageVersionRepository;
            this.exceptionHandler = exceptionHandler;
        }

        public GetPageResponse GetPage(int id, string languageCode = "en-gb")
        {
            var response = new GetPageResponse();

            try
            {
                var pageVersion = this.pageVersionRepository.Get().SingleOrDefault(x => x.PageId == id && x.LanguageCode == languageCode);
                if (pageVersion != null)
                {
                    response.Page = new PageDto
                    {
                        Title = pageVersion.Title,
                        Description = pageVersion.Description,
                        Keywords = pageVersion.Keywords
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

        public async Task<GetPageResponse> GetPageAsync(int id, string languageCode = "en-gb")
        {
            var response = new GetPageResponse();

            try
            {
                var pageVersion = await this.pageVersionRepository.Get().SingleOrDefaultAsync(x => x.PageId == id && x.LanguageCode == languageCode);
                if (pageVersion != null)
                {
                    response.Page = new PageDto
                    {
                        Title = pageVersion.Title,
                        Description = pageVersion.Description,
                        Keywords = pageVersion.Keywords
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
    }
}
