namespace MVC.WebUI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MVC.Core.Data.EntityFramework;
    using MVC.Core.Exceptions;
    using Services.Page;
    //using MVC.Services.Account;
    //using MVC.Services.Article;
    //using MVC.Services.Message;
    using Ninject;

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
            //this.kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            //this.kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            //this.kernel.Bind<IAccountService>().To<SimpleMembershipAdapter>();
            //this.kernel.Bind<IArticleService>().To<ArticleService>();
            //this.kernel.Bind<IMessageService>().To<EmailService>();
            this.kernel.Bind<IPageService>().To<PageService>();
			this.kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
#if DEBUG
			this.kernel.Bind<IExceptionHandler>().To<NullExceptionHandler>();
#else
			this.kernel.Bind<IExceptionHandler>().To<EmailExceptionHandler>();
			this.kernel.Bind<ISystemSettings>().To<SystemSettings>();
#endif
			//kernel.BindFilter<GlobalErrorAttribute>(FilterScope.Action, 0);
		}
	}
}