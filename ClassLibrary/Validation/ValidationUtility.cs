namespace ClassLibrary.Validation
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Schema;
    using ClassLibrary.Text.RegularExpressions;

    public static class ValidationUtility
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email.Trim(), string.Format("^{0}$", RegexUtility.Email), RegexOptions.IgnoreCase);
        }

        public static bool IsValidUKPostCode(string postCode)
        {
            if (string.IsNullOrWhiteSpace(postCode)) return false;
            postCode = postCode.Replace(" ", "");
            return Regex.IsMatch(postCode, string.Format("^{0}$", RegexUtility.UkPostCode), RegexOptions.IgnoreCase);
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            return Regex.IsMatch(name.Trim(), string.Format("^{0}$", RegexUtility.Name), RegexOptions.IgnoreCase);
        }

        public static bool IsValidLanguageCode(string languageCode)
        {
            return Regex.IsMatch(languageCode, "^[a-z]{2}-[a-z]{2}$");
        }

        public static bool IsValidDomainName(string domain)
        {
            Uri uri;
            return Uri.TryCreate(domain, UriKind.Absolute, out uri);
        }

        public static bool IsValidIsbn10(string isbn)
        {
            if (!string.IsNullOrWhiteSpace(isbn))
            {
                /*
                    CHECK DIGIT = 11 - (10x1 + 9x2 + 8x3 + 7x4 + 6x5 + 5x6 + 4x7 + 3x8 + 2x9) mod 11
                    if the result is 10, then an 'X' should be used
                 */

                string pattern = "^(?=.{13})(?!.{14})\\d{1,5}-\\d{1,8}-\\d{1,8}-[\\dxX]$";

                //CHECK THE ISBN IS IN THE CORRECT FORMAT
                if (Regex.IsMatch(isbn, pattern))
                {
                    //VALIDATE CHECK DIGIT

                    //remove all hyphens
                    isbn = isbn.Replace("-", "");

                    var checkDigit = GetIsbn10CheckDigit(isbn);

                    //compare the check digit to the last character in the ISBN
                    string lastChar = isbn.Substring(9, 1);
                    if (checkDigit == 10)
                    {
                        if (lastChar.ToUpper() == "X")
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (checkDigit == 11)
                        {
                            checkDigit = 0;
                        }

                        if (lastChar == checkDigit.ToString())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsValidIsbn13(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;
            //CHECK DIGIT = 10 - (x1 + 3x2 + x3 + 3x4 + ... + x11 + 3x12) mod 10

            string pattern = "^(?=.{17})(?!.{18})\\d{3}-\\d{1,5}-\\d{1,8}-\\d{1,8}-\\d$";

            if (Regex.IsMatch(isbn, pattern))
            {
                //remove all hyphens
                isbn = isbn.Replace("-", "");

                var checkDigit = GetIsbn13CheckDigit(isbn);

                //compare the check digit to the last character in the ISBN
                string lastChar = isbn.Substring(12, 1);
                if (lastChar == checkDigit.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidXml(string xml, string xsdFilePath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, xsdFilePath);

            using (XmlReader r = XmlReader.Create(new StringReader(xml), settings))
            {
                try
                {
                    while (r.Read()) ;
                }
                catch (XmlException)
                {
                    return false;
                }
                catch (XmlSchemaValidationException)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidCreditCardNumber(string creditCardNumber)
        {
            // http://www.codeproject.com/tips/515367/validate-credit-card-number-with-mod-algorithm

            if (!string.IsNullOrWhiteSpace(creditCardNumber))
            {
                if (Regex.IsMatch(creditCardNumber, @"^\d{12,19}$"))
                {
                    /*
                    1. Starting with the check digit double the value of every other digit 
                    2. If doubling of a number results in a two digits number, add up the digits to get a single digit number. This will results in eight single digit numbers                    
                    3. Get the sum of the digits
                    */
                    int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                                    .Reverse()
                                    .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                                    .Sum((e) => e / 10 + e % 10);


                    // If the final sum is divisible by 10, then the credit card number is valid. If it is not divisible by 10, the number is invalid.            
                    return sumOfDigits % 10 == 0;
                }
            }

            return false;
        }

        private static int GetIsbn10CheckDigit(string isbn)
        {
            int checkDigit = 0;

            for (int i = 0; i <= 8; i++)
            {
                checkDigit += (10 - i) * Convert.ToByte(isbn.Substring(i, 1));
            }

            checkDigit = 11 - (checkDigit) % 11;

            return checkDigit;
        }

        private static int GetIsbn13CheckDigit(string isbn)
        {
            int checkDigit = 0;

            for (int i = 0; i <= 11; i++)
            {
                if (i % 2 == 1)
                {
                    checkDigit += 3 * (Convert.ToByte(isbn.Substring(i, 1)));
                }
                else
                {
                    checkDigit += Convert.ToByte(isbn.Substring(i, 1));
                }
            }

            checkDigit = 10 - (checkDigit) % 10;

            return checkDigit;
        }
    }
}
