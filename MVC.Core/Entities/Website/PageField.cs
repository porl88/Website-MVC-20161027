namespace MVC.Core.Entities.Website
{
    public class PageField : BaseEntity
    {
        public int PageId { get; set; }

        public string Type { get; set; }

        public string Label { get; set; }
    }
}
