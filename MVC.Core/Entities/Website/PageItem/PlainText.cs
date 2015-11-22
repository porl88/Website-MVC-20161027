namespace MVC.Core.Entities.Website.PageItem
{
    using System.ComponentModel.DataAnnotations;

    public class PlainText : BaseEntity, IPageItem
    {
        [Required]
        public string Label { get; set; }

        [Required]
        public int PageId { get; set; }

        [Required]
        public int PageVersionId { get; set; }

        [Required, MaxLength(300)]
        public string Text { get; set; }
    }
}
