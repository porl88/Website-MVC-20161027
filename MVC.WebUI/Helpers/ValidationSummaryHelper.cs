namespace MVC.WebUI.Helpers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Xml.Linq;

    // http://stackoverflow.com/questions/12107263/why-is-validationsummarytrue-displaying-an-empty-summary-for-property-errors
    public static class ValidationSummaryHelper
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            var html = htmlHelper.ValidationSummary(excludePropertyErrors);

            if (html != null)
            {
                var msgHtml = XElement.Parse(html.ToHtmlString());
                var list = msgHtml.Element("ul").Elements("li");
                if (list.Count() == 1 && list.First().Value == "")
                {
                    return null;
                }
            }

            return html;
        }
    }
}