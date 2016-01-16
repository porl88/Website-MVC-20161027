namespace MVC.WebUI.Models.Account
{
	using System.ComponentModel.DataAnnotations;

	public class LoginViewModel
	{
		[Display(Name = "User Name")]
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}