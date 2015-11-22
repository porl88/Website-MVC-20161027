namespace MVC.Services.HTMLTemplate
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
	using ClassLibrary.Text;

    public abstract class HtmlTemplate : IHtmlTemplate
    {
        protected abstract string BaseTemplatePath { get; }

        protected abstract Dictionary<string, string> InnerTemplatePaths { get; }

        protected abstract Dictionary<string, object> Mappings { get; }

        public string GetHtml()
        {
            var html = this.GetTemplateHtml(this.BaseTemplatePath);

            html = this.AddInnerTemplateHtml(html, this.InnerTemplatePaths);

            html = this.AddMappings(html, this.Mappings);

            html = this.RemoveUnusedMarkers(html);

            return html;
        }

        private string GetTemplateHtml(string templatePath)
        {
            string html = string.Empty;

            if (File.Exists(templatePath))
            {
                html = File.ReadAllText(templatePath);
            }

            return html;
        }

        private string AddInnerTemplateHtml(string html, Dictionary<string, string> templatePaths)
        {
            if (templatePaths != null)
            {
                foreach (var path in templatePaths)
                {
                    var innerHtml = this.GetTemplateHtml(path.Value);
                    html = html.Replace(path.Key, innerHtml);
                }
            }

            return html;
        }

        private string AddMappings(string html, Dictionary<string, object> mappings)
        {
            if (mappings != null && !string.IsNullOrWhiteSpace(html))
            {
                foreach (var item in mappings)
                {
                    if (item.Value != null)
                    {
                        html = html.Replace(item.Key, item.Value.ToString());
                    }
                }
            }

            return html;
        }

        private string RemoveUnusedMarkers(string html)
        {
            html = Regex.Replace(html, @"##[A-Z]*##", string.Empty);
            return html;
        }

        protected string ConvertToXhtml(string text)
        {
			return StringUtility.ConvertPlainTextToHtml(text);
        }
    }
}
