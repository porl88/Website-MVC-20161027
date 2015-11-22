namespace WebUI.Helpers.Paging
{
	using System;
	using System.Text;
	using System.Web.Mvc;

	public class PagingInfo
	{
		public int TotalItems { get; set; }

		public int ItemsPerPage { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages
		{
			get
			{
				return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
			}
		}

		public string PreviousText { get; set; }

		public string NextText { get; set; }
	}

	// @Html.PageLinks(Model.PagingInfo, x => Url.Action("List", new { page = x }))

	public static class PagingHelpers
	{
		public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
		{
			var result = new StringBuilder();
			var totalPages = pagingInfo.TotalPages;
			var currentPage = pagingInfo.CurrentPage;

			result.Append("<nav class=\"pager\">");

			// previous link
			if (!string.IsNullOrWhiteSpace(pagingInfo.PreviousText) && currentPage > 1 && totalPages > 1)
			{
				// create anchor tag
				var tag = new TagBuilder("a");
				tag.MergeAttribute("href", pageUrl(currentPage - 1));
				tag.MergeAttribute("rel", "prev");
				tag.InnerHtml = pagingInfo.PreviousText;
				result.Append(tag.ToString());
			}

			// numbered links
			for (int i = 1; i <= totalPages; i++)
			{
				if (i == pagingInfo.CurrentPage)
				{
					// create span tag
					var tag = new TagBuilder("span");
					tag.AddCssClass("selected");
					tag.InnerHtml = i.ToString();
					result.Append(tag.ToString());
				}
				else
				{
					// create anchor tag
					var tag = new TagBuilder("a");
					tag.MergeAttribute("href", pageUrl(i));
					tag.InnerHtml = i.ToString();
					result.Append(tag.ToString());
				}
			}

			// next link
			if (!string.IsNullOrWhiteSpace(pagingInfo.NextText) && currentPage < totalPages && totalPages > 1)
			{
				// create anchor tag
				var tag = new TagBuilder("a");
				tag.MergeAttribute("href", pageUrl(currentPage + 1));
				tag.MergeAttribute("rel", "next");
				tag.InnerHtml = pagingInfo.NextText;
				result.Append(tag.ToString());
			}

			result.Append("</nav>");

			return MvcHtmlString.Create(result.ToString());
		}
	}
}