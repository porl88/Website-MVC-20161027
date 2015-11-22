namespace ClassLibrary.Text
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Text.RegularExpressions;
	using ClassLibrary.Text.RegularExpressions;

	public static class StringUtility
	{
		/// <summary>
		/// Removes unnecessary white space: allows only 1 space between words and trims spaces from the beginning and end of a phrase.
		/// </summary>
		/// <param name="text">Original text, containing white space.</param>
		/// <returns>string</returns>
		public static string RemoveWhiteSpace(this string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = Regex.Replace(text, "[\f\n\r\t\v]", string.Empty, RegexOptions.Singleline);
				return Regex.Replace(text.Trim(), " {2,}", " ");
			}

			return string.Empty;
		}

		/// <summary>
		/// Trims and removes extraneous spaces, and then truncates the phrase if still longer than the maximum length specified. Used for data submitted by an end-user via HTTP POST or GET.
		/// </summary>
		/// <param name="phrase">The original untruncated string.</param>
		/// <param name="maxLength">The maximum number of characters allowed in the string.</param>
		/// <returns>string</returns>
		public static string MaxLength(this string phrase, int maxLength)
		{
			if (!string.IsNullOrWhiteSpace(phrase))
			{
				phrase = phrase.RemoveWhiteSpace();

				// truncate if longer than the max amount
				if (phrase.Length > maxLength)
				{
					phrase = phrase.Remove(maxLength);
				}

				return phrase;
			}

			return string.Empty;
		}

		/// <summary>
		/// Converts text into title case: capitalises first letter of ALL words and ensures the rest are lower case.
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string Capitalise(this string text)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
		}

		/// <summary>
		/// Converts a phrase to Title Case - leaves articles, prepositions, and conjunctions as lower case
		/// </summary>
		/// <param name="phrase"></param>
		/// <returns></returns>
		public static string ToTitleCase(this string text)
		{
			return ToTitleCase(text, null);
		}

		public static string ToTitleCase(this string text, params string[] upperCaseWords)
		{
			//test text
			//Dim text As String = "  oN tHE bACK tO bdack (pC)      br'S Vip viPx vIps vipst  8gb 2x am2  wAlTon-sur-LA-mEre   kINGSTON-uPON-tHAMES. (cARROT) (iN)   voips  qpr  anc BANG  ok  fOR           it   i.t. fORT mCwILLIAM oF u.k. uK etc.  d'aRbY  o'nEILL'S"

			if (!string.IsNullOrWhiteSpace(text))
			{
				// CAPITALISE ALL WORDS
				text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

				//EXCEPTIONS: LOWER CASE WORDS

				//lists of words to convert to lower case
				string[] articles = { "a", "an", "the" };
				string[] conjunctions = { "and", "but", "nor", "or" };
				string[] prepositions =
                {
						"aboard","about","above","absent","across","after","against","along","alongside","amid","amidst","among",
						"amongst","around","as","aside","astride","at","athwart","atop",
						"barring","before","behind","below","beneath","beside","besides","between","betwixt","beyond","but","by",
						"circa","concerning",
						"despite","down","during",
						"except","excluding",
						"failing","following","for","from",
						"given",
						"in","including","inside","into",
						"like",
						"mid","minus",
						"near","next","notwithstanding",
						"of","off","on","onto","opposite","out","outside","over",
						"pace","past","per","plus","pro",
						"qua",
						"regarding","round",
						"save","since",
						"than","through","thru","throughout","till","times","to","toward","towards",
						"under","underneath","unlike","until","up","upon",
						"versus","vs","via","vice",
						"with","within","without","worth"
					};
				string[] formsToBe = { "be", "am", "is", "are", "being", "was", "were", "been" };
				string[] misc = { "some", "any", "yet", "so", "etc", "de", "la", "le", "sur" };

				//add all string arrays to a string collection
				var arrLowerCaseWords = new List<string>();
				arrLowerCaseWords.AddRange(articles);
				arrLowerCaseWords.AddRange(conjunctions);
				arrLowerCaseWords.AddRange(prepositions);
				arrLowerCaseWords.AddRange(formsToBe);
				arrLowerCaseWords.AddRange(misc);

				//create the regular expression from the string collection
				StringBuilder regExBuilder = new StringBuilder();
				foreach (string item in arrLowerCaseWords)
				{
					regExBuilder.AppendFormat("|(?!^)\\b{0}\\b", item);
				}

				var regExp = new Regex(regExBuilder.ToString().Substring(1), RegexOptions.IgnoreCase);
				text = regExp.Replace(text, m => m.Value.ToLower());

				//EXCEPTIONS: UPPER CASE WORDS
				//upper case expressions
				StringBuilder upperCaseRegEx = new StringBuilder();
				upperCaseRegEx.Append("\\b\\w*\\d\\w*\\b|");

				//any word containing a digit
				upperCaseRegEx.Append("\\b(?!(Mr|Mrs|Ms|Ltd)\\b)[bcdfghjklmnpqrstvwxz]{2,}\\b|");

				//words with no vowels (assumed to be acronyms)
				upperCaseRegEx.Append("\\b[bcdfghjklmnqrtvwxz][bdfgjkmpqsvxz]\\w+\\b|");

				//words beginning with certain consonant combinations (assumed to be acronyms)
				upperCaseRegEx.Append("(?<=\\bMc)\\w|");

				//words beginning with Mc - e.g. McDonald
				upperCaseRegEx.Append("(?<=\\b[OD]')\\w|");
				//words beginning with O' or d' - e.g. O'Neill's, D'Arby

				//upper case words
				string[] arrUpperCaseWords = { "OK", "UK", "USA", "PO", "VIP", "USB", "IP", "ROM" };
				upperCaseRegEx.AppendFormat("\\b{0}(?=s?\\b)|", string.Join("(?=s?\\b)|\\b", arrUpperCaseWords));

				//add user-defined upper case words
				if (upperCaseWords != null)
				{
					upperCaseRegEx.AppendFormat("\\b{0}(?=s?\\b)", string.Join("(?=s?\\b)|\\b", upperCaseWords));
				}

				regExp = new Regex(upperCaseRegEx.ToString(), RegexOptions.IgnoreCase);
				text = regExp.Replace(text, m => m.Value.ToUpper());

				//EXCEPTION: SPECIFIC COMBINATIONS
				text = text.Replace("Voip", "VoIP");
			}

			return text;
		}

		/// <summary>
		/// Check whether a string is in upper case or not. Returns false if it finds any lower case letters.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsUpperCase(this string text)
		{
			return !text.Any(x => char.IsLower(x) == true);
		}

		/// <summary>
		/// Check whether a string is in lower case or not. Returns false if it finds any upper case letters.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsLowerCase(this string text)
		{
			return !text.Any(x => char.IsUpper(x) == true);
		}

		public static string Base64Encode(this string text)
		{
			byte[] encbuff = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(encbuff);
		}

		public static string Base64Decode(this string text)
		{
			byte[] decbuff = Convert.FromBase64String(text);
			return Encoding.UTF8.GetString(decbuff);
		}

		public static string ConvertPlainTextToHtml(string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				var lineBreakPattern = "(\r?\n|\r)";
				var html = WebUtility.HtmlEncode(text);
				html = Regex.Replace(html, lineBreakPattern + "{2,}", "</p><p>");
				html = Regex.Replace(html, lineBreakPattern, "<br />");
				html = ConvertUrlsToHyperLinks(html);
				html = string.Format("<p>{0}</p>", html);
				return html.RemoveWhiteSpace();
			}

			return string.Empty;
		}

		private static string ConvertUrlsToHyperLinks(string text)
		{
			return Regex.Replace(text, RegexUtility.Uri, x => ConvertUrlToHyperLink(x.Value));
		}

		private static string ConvertUrlToHyperLink(string text)
		{
			Uri uri;
			if (Uri.TryCreate(text, UriKind.Absolute, out uri))
			{
				text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}{2}</a>", uri.AbsoluteUri, uri.Host, uri.PathAndQuery);
			}

			return text;
		}
	}
}
