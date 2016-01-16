namespace MVC.WebUI.Models.Account
{
	using System.ComponentModel.DataAnnotations;
    using Core.Configuration;

	public class RegisterViewModel
	{
		private string email;

		[Display(Name = "User Name")]
		[Required, MaxLength(30)]
		public string UserName { get; set; }

		[Required, MaxLength(256), EmailAddress]
		public string Email
		{
			get
			{
				return this.email;
			}

			set
			{
                this.email = value;
				//this.email = FormatterUtility.Email(value);
			}
		}

		[Required, RegularExpression(RegularExpressions.PasswordWeak, ErrorMessage = "'{0}' must be at least 8 characters long and cannot contain spaces.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}