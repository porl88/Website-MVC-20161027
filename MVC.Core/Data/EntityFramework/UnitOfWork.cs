namespace MVC.Core.Data.EntityFramework
{
    using System.Threading.Tasks;
    using Entities.Article;
    using Entities.Website;
    using Entities.Website.PageItem;
    using Entities.Culture;
    using Entities.Account;

    public class UnitOfWork : IUnitOfWork
	{
		private readonly WebsiteDbContext context = new WebsiteDbContext();
		private readonly IRepository<Article> articleRepository;
        private readonly IRepository<ArticleVersion> articleVersionRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<Page> pageRepository;
        private readonly IRepository<PageVersion> pageVersionRepository;
        private readonly IRepository<PlainText> plainTextRepository;
        private readonly IRepository<RichText> richTextRepository;
        private readonly IRepository<User> userRepository;

        public UnitOfWork()
		{
            // http://www.codeproject.com/Tips/814618/Use-of-Database-SetInitializer-method-in-Code-Firs
#if DEBUG
            //this.context.Database.Initialize(false);
#endif
            this.articleRepository = new Repository<Article>(this.context);
            this.articleVersionRepository = new Repository<ArticleVersion>(this.context);
            this.languageRepository = new Repository<Language>(this.context);
            this.pageRepository = new Repository<Page>(this.context);
            this.pageVersionRepository = new Repository<PageVersion>(this.context);
            this.plainTextRepository = new Repository<PlainText>(this.context);
            this.richTextRepository = new Repository<RichText>(this.context);
            this.userRepository = new Repository<User>(this.context);
        }

        public IRepository<Article> ArticleRepository
		{
			get { return this.articleRepository; }
		}

        public IRepository<ArticleVersion> ArticleVersionRepository
        {
            get { return this.articleVersionRepository; }
        }

        public IRepository<Language> LanguageRepository
        {
            get { return this.languageRepository; }
        }

        public IRepository<Page> PageRepository
        {
            get { return this.pageRepository; }
        }

        public IRepository<PageVersion> PageVersionRepository
        {
            get { return this.pageVersionRepository; }
        }

        public IRepository<PlainText> PlainTextRepository
        {
            get { return this.plainTextRepository; }
        }

        public IRepository<RichText> RichTextRepository
        {
            get { return this.richTextRepository; }
        }

        public IRepository<User> UserRepository
        {
            get { return this.userRepository; }
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

		public async Task CommitAsync()
		{
			await this.context.SaveChangesAsync();
		}
	}
}
