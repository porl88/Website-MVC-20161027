namespace MVC.Core.Entities.Culture
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Language : BaseEntity
	{
        [Required, RegularExpression("^[a-z]{2}-[a-z]{2}$", ErrorMessage = "Must be a valid language code - e.g. en-gb, fr-fr.")]
        //[Index(IsUnique = true)]
        public string LanguageCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string DialectName { get; set; }

        [Required]
        public string LocalName { get; set; }

        public string LocalDialectName { get; set; }
    }
}
