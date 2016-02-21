namespace MVC.Core.Data.EntityFramework
{
    using System.Threading.Tasks;
    using Core.Entities.Article;
    using Entities.Website;
    using Entities.Website.PageItem;
    using Entities.Culture;
    using Entities.Account;

    public interface IUnitOfWork
	{
		IRepository<Article> ArticleRepository { get; }

        IRepository<ArticleVersion> ArticleVersionRepository { get; }

        IRepository<Language> LanguageRepository { get; }

        IRepository<Page> PageRepository { get; }

        IRepository<PageVersion> PageVersionRepository { get; }

        IRepository<PlainText> PlainTextRepository { get; }

        IRepository<RichText> RichTextRepository { get; }

        IRepository<User> UserRepository { get; }

        void Commit();

		Task CommitAsync();
	}
}
