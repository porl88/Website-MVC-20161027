namespace MVC.Core.Testing
{
    using System.Threading.Tasks;
    using MVC.Core.Data.EntityFramework;
    using MVC.Core.Entities.Article;
    using Entities.Website;
    using Entities.Website.PageItem;
    using Entities.Culture;
    using Entities.Account;

    public class MockUnitOfWork : IUnitOfWork
	{
		private readonly IRepository<Article> articleRepository;
        private readonly IRepository<ArticleVersion> articleVersionRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<Page> pageRepository;
        private readonly IRepository<PageVersion> pageVersionRepository;
        private readonly IRepository<PlainText> plainTextRepository;
        private readonly IRepository<RichText> richTextRepository;
        private readonly IRepository<User> userRepository;

        public MockUnitOfWork()
		{
			this.articleRepository = new MockRepository<Article>();
            this.articleVersionRepository = new MockRepository<ArticleVersion>();
            this.languageRepository = new MockRepository<Language>();
            this.pageRepository = new MockRepository<Page>();
            this.pageVersionRepository = new MockRepository<PageVersion>();
            this.plainTextRepository = new MockRepository<PlainText>();
            this.richTextRepository = new MockRepository<RichText>();
            this.userRepository = new MockRepository<User>();
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
		}

		public async Task CommitAsync()
		{
		}
	}
}
