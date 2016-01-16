namespace MVC.Core.Entities.Culture
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Language : BaseEntity
	{
        public Language()
        {
            this.LanguageVersions = new List<LanguageVersion>();
        }

        public virtual List<LanguageVersion> LanguageVersions { get; set; }

        //[Required]
        //public string Name { get; set; }

        //public string Dialect { get; set; }

        [Required, RegularExpression("^[a-z]{2}-[a-z]{2}$", ErrorMessage = "Must be a valid language code - e.g. en-gb, fr-fr.")]
        //[Index(IsUnique = true)]
        public string LanguageCode { get; set; }
    }
}
