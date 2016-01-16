namespace MVC.WebUI.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using Core.Configuration;

    public class ResetPasswordViewModel
	{
		[Required, RegularExpression(RegularExpressions.PasswordWeak, ErrorMessage = "'{0}' must be at least 8 characters long and cannot contain spaces.")]
		public string NewPassword { get; set; }

		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

        public string ResetPasswordToken { get; set; }
	}
}