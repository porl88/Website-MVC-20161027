namespace MVC.Core.Entities.Article
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ArticleVersion : BaseEntity
    {
        [Required]
        public int ArticleId { get; set; }

        [Required, RegularExpression("^[a-z]{2}-[a-z]{2}$", ErrorMessage = "Must be a valid language code - e.g. en-gb, fr-fr.")]
        public string LanguageCode { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTimeOffset? Published { get; set; }
    }
}
