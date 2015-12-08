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
        public Language Language { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTimeOffset? Published { get; set; }
    }
}
