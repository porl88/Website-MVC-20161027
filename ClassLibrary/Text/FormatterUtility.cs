namespace ClassLibrary.Text
{
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Text.RegularExpressions;
    using ClassLibrary.Validation;

    public static class FormatterUtility
    {
		/// <summary>
        /// Formats a valid UK post code to upper case with a space between the outward and inward parts of the code.
        /// </summary>
        /// <param name="postCode"></param>
        /// <returns></returns>
        public static string UKPostCode(string postCode)
        {
            if (ValidationUtility.IsValidUKPostCode(postCode))
            {
                postCode = postCode.Replace(" ", "");
                postCode = postCode.ToUpper().Insert(postCode.Length - 3, " ");
            }

            return postCode;
        }

        public static string Email(string email)
        {
            return email.Trim().ToLower();
        }

        public static string UKTel(string tel)
        {
            tel = Regex.Replace(tel, @"\D", ""); // remove anything that's not a number
            string formattedTel = tel.TrimStart('0'); // remove initial zero

            var len = formattedTel.Length;

            switch (len)
            {
                case 10:
                    if (Regex.IsMatch(formattedTel, @"^(2\d|55|56|70|76)\d+$"))
                    {
                        formattedTel = formattedTel.Insert(len - 4, " ");
                        formattedTel = formattedTel.Insert(formattedTel.Length - 9, " ");
                    }
                    else
                    {
                        formattedTel = formattedTel.Insert(len - 4, " ");
                        formattedTel = formattedTel.Insert(formattedTel.Length - 8, " ");
                    }
                    break;
                case 9:
                    if (Regex.IsMatch(formattedTel, @"^16977\d+$"))
                    {
                        formattedTel = formattedTel.Insert(len - 4, " ");
                    }
                    else if (Regex.IsMatch(formattedTel, @"^1\d+$"))
                    {
                        formattedTel = formattedTel.Insert(len - 5, " ");
                    }
                    else
                    {
                        formattedTel = formattedTel.Insert(len - 6, " ");
                    }
                    break;
                case 8:
                    if (Regex.IsMatch(formattedTel, @"^845\d+$"))
                    {
                        formattedTel = formattedTel.Insert(len - 2, " ");
                        formattedTel = formattedTel.Insert(formattedTel.Length - 5, " ");
                    }
                    else
                    {
                        formattedTel = formattedTel.Insert(len - 4, " ");
                    }
                    break;
            }

            if (tel.StartsWith("0")) formattedTel = "0" + formattedTel; // restore initial zero if there was one originally
            return formattedTel;
        }

        /// <summary>
        /// Converts a number to its equivalent Roman Numeral.
        /// </summary>
        /// <param name="shoNumber"></param>
        /// <returns></returns>
        public static string ToRomanNumerals(int number)
        {
            string romanNo;
            char[] arrNos = number.ToString().ToCharArray();

            //reverse the order
            Array.Reverse(arrNos);

            //first ordinal
            switch (arrNos[0])
            {
                case '1':
                    romanNo = "I";
                    break;
                case '2':
                    romanNo = "II";
                    break;
                case '3':
                    romanNo = "III";
                    break;
                case '4':
                    romanNo = "IV";
                    break;
                case '5':
                    romanNo = "V";
                    break;
                case '6':
                    romanNo = "VI";
                    break;
                case '7':
                    romanNo = "VII";
                    break;
                case '8':
                    romanNo = "VIII";
                    break;
                case '9':
                    romanNo = "IX";
                    break;
                default:
                    romanNo = string.Empty;
                    break;
            }

            //second ordinal
            switch (arrNos[1])
            {
                case '1':
                    romanNo = "X" + romanNo;
                    break;
                case '2':
                    romanNo = "XX" + romanNo;
                    break;
                case '3':
                    romanNo = "XXX" + romanNo;
                    break;
                case '4':
                    romanNo = "XL" + romanNo;
                    break;
                case '5':
                    romanNo = "L" + romanNo;
                    break;
                case '6':
                    romanNo = "LX" + romanNo;
                    break;
                case '7':
                    romanNo = "LXX" + romanNo;
                    break;
                case '8':
                    romanNo = "LXXX" + romanNo;
                    break;
                case '9':
                    romanNo = "XC" + romanNo;
                    break;
            }

            //third ordinal
            switch (arrNos[2])
            {
                case '1':
                    romanNo = "C" + romanNo;
                    break;
                case '2':
                    romanNo = "CC" + romanNo;
                    break;
                case '3':
                    romanNo = "CCC" + romanNo;
                    break;
                case '4':
                    romanNo = "CD" + romanNo;
                    break;
                case '5':
                    romanNo = "D" + romanNo;
                    break;
                case '6':
                    romanNo = "DC" + romanNo;
                    break;
                case '7':
                    romanNo = "DCC" + romanNo;
                    break;
                case '8':
                    romanNo = "DCCC" + romanNo;
                    break;
                case '9':
                    romanNo = "CM" + romanNo;
                    break;
            }

            //fourth ordinal
            switch (arrNos[3])
            {
                case '1':
                    romanNo = "M" + romanNo;
                    break;
                case '2':
                    romanNo = "MM" + romanNo;
                    break;
                case '3':
                    romanNo = "MMM" + romanNo;
                    break;
                //Case "4"c
                //	romanNo = "MM" & romanNo
                //Case "5"c
                //	romanNo = "C" & romanNo
                //Case "6"c
                //	romanNo = "C" & romanNo
                //Case "7"c
                //	romanNo = "C" & romanNo
                //Case "8"c
                //	romanNo = "C" & romanNo
                //Case "9"c
                //	romanNo = "C" & romanNo
            }

            return romanNo;
        }

        /// <summary>
        /// Formats a string dictionary for output into a plain text file. Creates a table by using spaces.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string FormatCollectionAsPlainText(string title, NameValueCollection collection)
        {
            StringBuilder output = new StringBuilder();
            short shoMaxLength = 0;

            //create header
            if (!string.IsNullOrWhiteSpace(title))
            {
                output.Append('=', 100);
                output.AppendLine();
                output.Append(title.ToUpper());
                output.AppendLine();
                output.Append('=', 100);
                output.AppendLine();
                output.AppendLine();
            }

            //work out longest key
            foreach (string key in collection.Keys)
            {
                if (key.Length > shoMaxLength)
                    shoMaxLength = Convert.ToInt16(key.Length);
            }

            //add extra margin of spaces
            shoMaxLength += 5;

            //add browser details
            foreach (string key in collection.Keys)
            {
                output.Append((key.ToUpper() + ":").PadRight(shoMaxLength));
                output.Append(collection[key]);
                output.AppendLine();
            }

            output.AppendLine();
            output.AppendLine();

            return output.ToString();
        }
    }
}
