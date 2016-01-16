namespace MVC.Core.Entities.Article
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Culture;

    public class ArticleVersion : BaseEntity
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }
    }
}
