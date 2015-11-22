namespace ClassLibrary.Text
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

    public class Markup
    {
        private readonly string markup;

        public Markup(string markup)
        {
            this.markup = markup;
        }

		/// <summary>
		/// Removes all HTML and extraneous spaces from a block of text.
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
        public string RemoveMarkup()
        {
			var text = this.markup;

			if (!string.IsNullOrWhiteSpace(text))
			{
				// strip out CDATA instructions
				text = text.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);

				// strip out <script> tags - including content
				text = Regex.Replace(text, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// strip out <style> tags - including content
				text = Regex.Replace(text, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// strip out HTML tags - including processing instructions
				text = Regex.Replace(text, "<[/!?]?[a-z]+[^>]*>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// strip out HTML comments
				text = Regex.Replace(text, "<!--.*?-->", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// remove extraneous spaces
				text = text.RemoveWhiteSpace();
			}

			return text;
        }

		/// <summary>
		/// Removes all HTML and extraneous spaces apart from specified tags, attributes and class names.
		/// </summary>
		/// <param name="html"></param>
		/// <param name="permittedTags"></param>
		/// <param name="permittedAttributes"></param>
		/// <param name="permittedClassNames"></param>
		/// <returns></returns>
		public string RemoveNonPermittedMarkup(string[] permittedTags, string[] permittedAttributes, string[] permittedClassNames)
		{
			var html = this.markup;

			if (!string.IsNullOrWhiteSpace(html))
			{
				string pattern;

				// strip out CDATA instructions
				html = html.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);

				// strip out <script> tags - including content
				if (!permittedTags.Contains("script"))
				{
					pattern = "<script.*?</script>";
					html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				}

				if (!permittedTags.Contains("button"))
				{
					pattern = "<button.*?</button>";
					html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				}

				// strip out HTML tags
				if (permittedTags != null)
				{
					pattern = string.Format("<[/!]?(?!({0})[ >])[a-z]+[^>]*>", String.Join("|", permittedTags));
					html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				}

				// strip out HTML comments
				pattern = "<!--.*?-->";
				html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// strip out HTML attributes
				if (permittedAttributes == null)
				{
					pattern = @" +[a-z-]+=""[^""]*""";
				}
				else
				{
					pattern = string.Format(@" +(?!({0}))[a-z-]+=""[^""]*""", string.Join("|", permittedAttributes));
				}

				html = Regex.Replace(html, pattern, string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// strip out class names (if specified)
				if (permittedClassNames != null)
				{
					pattern = string.Format(@" *class=""(?!({0}))[^""]*""", string.Join("|", permittedClassNames));
					html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				}

				// strip out empty tags
				pattern = @"<(?'a'[a-z]+)[^>]*>\s*</ *\k'a' *>";
				html = Regex.Replace(html, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// correct any unclosed self-closing tags
				pattern = @"(<(area|base|br|col|command|embed|hr|img|input|keygen|link|meta|param|source|track|wbr)[^<]*[^/])>";
				html = Regex.Replace(html, pattern, "$1/>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				// remove extraneous spaces
				html = html.RemoveWhiteSpace();

				// strip out spaces next to tags
				pattern = @"/ *(</?[a-z][^>]*>) *";
				html = Regex.Replace(html, pattern, string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
			}

			return html;
		}

		public List<string> GetImageUrlsFromMarkup()
		{
			var pattern = "<img .*?src=\"(.*?)\"";
			return Regex.Matches(this.markup, pattern, RegexOptions.IgnoreCase).Cast<Match>().Select(x => x.Groups[1].Value).ToList();
		}
    }
}
