namespace ClassLibrary.Text
{
    using System;

	public static class MarkupUtility
    {
        public static string RemoveMarkup(this string markup)
        {
			var markupUtility = new Markup(markup);
			return markupUtility.RemoveMarkup();
        }
    }
}
