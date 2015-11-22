namespace MVC.Core.Entities.Website
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PageVersion : BaseEntity
	{
        [Required]
        public int PageId { get; set; }

        [Required, RegularExpression("^[a-z]{2}-[a-z]{2}$", ErrorMessage = "Must be a valid language code - e.g. en-gb, fr-fr.")]
        public string LanguageCode { get; set; }

        [Required, MaxLength(70), RegularExpression("^[a-z]+(-[a-z]+)*[a-z]$", ErrorMessage = "Must be a valid URL segment.")]
        public string UrlSegment { get; set; }

        [Required, MaxLength(70)]
        public string Title { get; set; }

        [MaxLength(160)]
        public string Description { get; set; }

        [MaxLength(160)]
        public string Keywords { get; set; }

        public virtual List<IPageItem> PageItems { get; set; }
    }
}
