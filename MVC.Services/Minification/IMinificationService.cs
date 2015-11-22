namespace MVC.Services.Minification
{
	public interface IMinificationService
	{
		string MinifyJavascript(string text);

		string MinifyCss(string text);
	}
}
