namespace MVC.Core.Entities.Article
{
    using System;
    using System.Collections.Generic;

    public class Article : BaseEntity
    {
        public Article()
        {
            this.ArticleVersions = new List<ArticleVersion>();
        }

        public virtual List<ArticleVersion> ArticleVersions { get; set; }

        public DateTimeOffset? Published { get; set; }
    }
}
