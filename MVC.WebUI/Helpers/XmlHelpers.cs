namespace WebUI.Helpers.Xml
{
	using System.IO;
	using System.Web;
	using System.Web.Mvc;
	using System.Xml;
	using System.Xml.Xsl;

	public static class XmlHelpers
	{
		public static MvcHtmlString XsltTransform(this HtmlHelper html, string xmlPath, string xsltPath, XsltArgumentList parameters = null)
		{
			var context = HttpContext.Current;
			var xslt = new XslCompiledTransform();
			xslt.Load(context.Server.MapPath(xsltPath));

			using (var reader = XmlReader.Create(context.Server.MapPath(xmlPath)))
			{
				using (var writer = new StringWriter())
				{
					xslt.Transform(reader, parameters, writer);
					return new MvcHtmlString(writer.ToString());
				}
			}
		}
	}
}