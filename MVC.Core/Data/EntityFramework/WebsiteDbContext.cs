namespace MVC.Core.Data.EntityFramework
{
    using System.Data.Entity;
    using Entities.Article;
    using Entities.Website;
    using Entities.Website.PageItem;
    using Entities.Culture;

    public class WebsiteDbContext : DbContext
	{
        public WebsiteDbContext()
        : base("WebsiteMvcDatabase") // connection string name
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleVersion> ArticleVersions { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<PageVersion> PageVersions { get; set; }

        public DbSet<PlainText> PlainTexts { get; set; }

        public DbSet<RichText> RichTexts { get; set; }
    }
}
