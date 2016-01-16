namespace MVC.Core.Entities.Culture
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageVersion : BaseEntity
    {
        public int LanguageId { get; set; }

        public int Language { get; set; }

        [Required]
        public string Name { get; set; }

        public string Dialect { get; set; }
    }
}
