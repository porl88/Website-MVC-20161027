namespace MVC.Core.Entities.Website
{
    public interface IPageItem
    {
        int PageId { get; set; }

        int PageVersionId { get; set; }

        string Label { get; set; }
    }
}
