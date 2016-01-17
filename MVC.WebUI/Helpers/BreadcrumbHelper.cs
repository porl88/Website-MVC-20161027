namespace MVC.WebUI.Helpers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class BreadcrumbHelper
    {
        public static MvcHtmlString Breadcrumb(this HtmlHelper html)
        {
            var controller = html.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            var action = html.ViewContext.RouteData.Values["action"].ToString().ToLower();

            if (controller == "home" && action == "index")
            {
                return MvcHtmlString.Empty;
            }

            var orderedList = new TagBuilder("ol");
            orderedList.AddCssClass("breadcrumb");

            var firstItem = new TagBuilder("li");
            firstItem.InnerHtml += html.ActionLink("Home", "Index", "Home");
            orderedList.InnerHtml += firstItem;

            var secondItem = new TagBuilder("li");
            if (action == "index")
            {
                secondItem.SetInnerText(controller);
            }
            else
            {
                secondItem.InnerHtml += html.ActionLink(controller, "Index", controller);
            }
            orderedList.InnerHtml += secondItem;

            if (action != "index")
            {
                var thirdItem = new TagBuilder("li");
                thirdItem.SetInnerText(action);
                orderedList.InnerHtml += thirdItem;
            }

            return MvcHtmlString.Create(orderedList.ToString());
        }
    }
}