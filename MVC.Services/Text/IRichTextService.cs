namespace MVC.Services.Text
{
	public interface IRichTextService
	{
		string TidyUpHtml(string xsltFilePath);

		bool ValidateHtml(string xsdFilePath);
	}
}
