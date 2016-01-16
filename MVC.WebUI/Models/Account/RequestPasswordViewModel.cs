namespace MVC.WebUI.Models.Account
{
	using System.ComponentModel.DataAnnotations;

	public class RequestPasswordViewModel
	{
		[Display(Name = "User Name")]
		[Required]
		public string UserName { get; set; }
	}
}