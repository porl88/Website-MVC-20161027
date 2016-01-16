namespace MVC.Services.Culture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Data.EntityFramework;
    using Core.Exceptions;
    using Core.Entities.Culture;
    using Transfer;

    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Language> languageRepository;
        private readonly IExceptionHandler exceptionHandler;

        public LanguageService(IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler)
        {
            this.unitOfWork = unitOfWork;
            this.languageRepository = unitOfWork.LanguageRepository;
            this.exceptionHandler = exceptionHandler;
        }

        public async Task<GetLanguagesResponse> GetLanguagesAsync(GetLanguagesRequest request)
        {
            var response = new GetLanguagesResponse();

            try
            {
                response.Languages = await this.unitOfWork.LanguageVersionRepository.GetAsync(q => q
                    .Where(x => x.Language == request.LanguageId)
                    .OrderBy(x => x.Name)
                    .Select(x => new LanguageDto
                    {
                        Id = x.LanguageId,
                        Name = x.Name
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
    }
}
