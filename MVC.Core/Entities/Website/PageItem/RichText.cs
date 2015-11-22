namespace MVC.Core.Entities.Website.PageItem
{
    using System.ComponentModel.DataAnnotations;

    public class RichText : BaseEntity, IPageItem
    {
        [Required]
        public string Label { get; set; }

        [Required]
        public int PageId { get; set; }

        [Required]
        public int PageVersionId { get; set; }

        [Required]
        public string Html { get; set; }
    }
}
