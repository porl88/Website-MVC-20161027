namespace MVC.Core.Configuration
{
    public static class FormPatterns
    {
        public const string Email = @" *[a-zA-Z\d!#\$%&'\*\+\-/=\?\^_`\{\|}~]+(\.[a-zA-Z\d!#\$%&'\*\+\-/=\?\^_`\{\|}~]+)*@[a-zA-Z\d]+([\.\-][a-zA-Z\d]+)*(\.[a-zA-Z]{2,}) *";

        public const string UkPostCode = @" *[a-zA-Z]{1,2}\d[a-zA-Z\d]? *\d[a-zA-Z]{2} *";

        public const string Name = @"(?=.*\w{2})[a-zA-Z '-]"; // must contain at least 2 consecutive letters

        public const string DomainName = @"(https?://\.)?(www\.)?(?!www\.)[a-z0-9-]+(\.[a-z]+)+";

        public static string Uri
        {
            get
            {
                return $@"{DomainName}(/[\w-])*(\?[^ ]*)+";
            }
        }

        public const string PasswordWeak = @"\S{8,}"; // min 8 characters, no white space characters

        public const string PasswordMedium = @"(?=(.*[a-z]){2})(?=(.*[A-Z\d\W_]){2})\S{8,}"; // min 8 characters, no white space, at least 2 lower case characters, must contain at least 2 of ANY combination of the following: uppercase characters OR digits OR special characters (e.g. ~$%#)

        public const string PasswordStrong = @"(?=.*[a-z])(?=.*[A-Z])(?=(.*\d))\S{8,}"; // min 8 characters, no white space, must contain at least 1 of EACH the following characters: lowercase characters AND uppercase characters AND digits

        public const string PasswordBest = @"(?=.*[a-z])(?=.*[A-Z])(?=(.*\d))(?=.*[\W_])\S{14,}"; // min 14 characters, no white space, must contain at least 1 of EACH the following characters: lowercase characters AND uppercase characters AND digits AND special characters (e.g. ~$%#)
    }
}
