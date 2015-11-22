namespace MVC.Services.Minification
{
	using System.Text.RegularExpressions;

	public class MinificationService : IMinificationService
	{
		public string MinifyJavascript(string text)
		{
			// remove /* ... */ comments
			text = Regex.Replace(text, @"(?<!/)/\*((?!\*/).)*\*/", string.Empty, RegexOptions.Singleline);

			// remove // comments - remove all text beginning with // unless it is a hyperlink
			text = Regex.Replace(text, @"(?<!https?:)//[^\n\r]*[\n\r]", string.Empty, RegexOptions.IgnoreCase);

			//remove all line breaks and tabs
			text = Regex.Replace(text, @"[\f\n\r\t\v]", string.Empty, RegexOptions.Singleline);

			//remove all extraneous spaces
			text = Regex.Replace(text, @" {2,}", string.Empty);

			// temporarily replace spaces with a character sequence in the following: string phrases ("" or ''), regular expressions (//)
			var evaluator = new MatchEvaluator(ReplaceSpaces);
			text = Regex.Replace(text, @"'[^']*'|""[^""]*""", evaluator);

			// remove any spaces around code characters - excluded: / (because it is used for regular expressions)
			var codeCharacters = "[+*=(){}<>!|,;:?&-]+";
			text = Regex.Replace(text, @"(?<!/) *(" + codeCharacters + ") *", "$1");

			// restore spaces
			text = text.Replace("~¬@~", " ");

			return text;
		}

		public string MinifyCss(string text)
		{
			// remove /* ... */ comments
			text = Regex.Replace(text, @"(?<!/)/\*((?!\*/).)*\*/", string.Empty, RegexOptions.Singleline);

			//remove all line breaks and tabs
			text = Regex.Replace(text, @"[\f\n\r\t\v]", string.Empty, RegexOptions.Singleline);

			//remove all extraneous spaces
			text = Regex.Replace(text, @" {2,}", string.Empty);

			// remove any spaces around code characters
			var codeCharacters = "[:;,{}>]+";
			text = Regex.Replace(text, @"(?<!['""]) *(" + codeCharacters + ") *", "$1");

			return text;
		}

		private string ReplaceSpaces(Match m)
		{
			return m.Value.Replace(" ", "~¬@~");
		}
	}
}
