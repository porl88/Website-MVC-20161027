namespace MVC.WebUI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Core.Configuration;
    using MVC.Core.Data.EntityFramework;
    using MVC.Core.Exceptions;
    using MVC.Services.Account;
    using Services.Storage;
    using Ninject;
    using Services.Article;
    using Services.Message;
    using Services.Page;
    using Services.Culture;
    using System.Web;

    public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver()
		{
			this.kernel = new StandardKernel();
			this.AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return this.kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
            this.kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            //this.kernel.Bind<IAccountService>().To<SimpleMembershipAdapter>();
            this.kernel.Bind<IAccountService>().To<IdentityAdapter>();
            this.kernel.Bind<IArticleService>().To<ArticleService>();
#if DEBUG
            this.kernel.Bind<IExceptionHandler>().To<NullExceptionHandler>();
#else
			this.kernel.Bind<IExceptionHandler>().To<EmailExceptionHandler>();
#endif
            this.kernel.Bind<ILanguageService>().To<LanguageService>();
            //this.kernel.Bind<ILoginService>().To<FormsAuthenticationAdapter>();
            this.kernel.Bind<ILoginService>().To<SimpleMembershipAdapter>();
            //this.kernel.Bind<ILoginService>().To<IdentityAdapter>();
            this.kernel.Bind<IMessageService>().To<EmailService>();
            this.kernel.Bind<IPageService>().To<PageService>();
            this.kernel.Bind<IPersistenceService>().To<GlobalCookiePersistenceService>();
            this.kernel.Bind<ISystemSettings>().To<SystemSettings>();
            this.kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            //this.kernel.BindFilter<GlobalErrorAttribute>(FilterScope.Action, 0);
        }
    }
}