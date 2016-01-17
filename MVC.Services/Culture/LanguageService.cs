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
    using Storage;

    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Language> languageRepository;
        private readonly IExceptionHandler exceptionHandler;
        private readonly IPersistenceService persistenceService;

        public LanguageService(IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler, IPersistenceService persistenceService)
        {
            this.unitOfWork = unitOfWork;
            this.languageRepository = unitOfWork.LanguageRepository;
            this.exceptionHandler = exceptionHandler;
            this.persistenceService = persistenceService;
        }

        public int GetPreferredLanguage()
        {
            var languageId = this.persistenceService.GetValue("lang") ?? "1";
            return Convert.ToInt32(languageId);
        }

        public void SetPreferredLanguage(int languageId)
        {
            this.persistenceService.SaveValue("lang", languageId.ToString());
        }

        public GetLanguagesResponse GetLanguages(GetLanguagesRequest request)
        {
            var response = new GetLanguagesResponse();

            try
            {
                response.Languages = this.unitOfWork.LanguageVersionRepository.Get(q => q
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
