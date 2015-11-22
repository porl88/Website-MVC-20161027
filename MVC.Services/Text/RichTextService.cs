namespace MVC.Services.Text
{
	using System.IO;
	using System.Xml;
	using System.Xml.Xsl;
	using ClassLibrary.Validation;

	public class RichTextService : IRichTextService
	{
		private readonly string richText;

		public RichTextService(string richText)
		{
			this.richText = richText;
		}

		public string TidyUpHtml(string xsltFilePath)
		{
			var xslt = new XslCompiledTransform();
			xslt.Load(xsltFilePath);

			var results = new StringWriter();

			using (XmlReader reader = XmlReader.Create(new StringReader(this.richText)))
			{
				xslt.Transform(reader, null, results);
			}

			return results.ToString();
		}

		public bool ValidateHtml(string xsdFilePath)
		{
			var html = string.Format("<body>{0}</body>", this.richText);
			return ValidationUtility.IsValidXml(html, xsdFilePath);
		}
	}
}
