namespace MVC.WebUI.Models.Account
{
	using System.ComponentModel.DataAnnotations;
    using Core.Configuration;

    public class ChangePasswordViewModel
	{
		[Display(Name = "Old Password")]
		[Required]
		public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required, RegularExpression(RegularExpressions.PasswordWeak, ErrorMessage = "'{0}' must be at least 8 characters long and cannot contain spaces.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}