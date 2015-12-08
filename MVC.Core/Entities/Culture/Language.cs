namespace MVC.Core.Entities.Culture
{
    using System.ComponentModel.DataAnnotations;

    public class Language : BaseEntity
	{
        [Required]
        public string Name { get; set; }

        [Required, RegularExpression("^[a-z]{2}-[a-z]{2}$", ErrorMessage = "Must be a valid language code - e.g. en-gb, fr-fr.")]
        public string LanguageCode { get; set; }
	}
}
