namespace MVC.WebUI.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UsersCreateViewModel
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }
    }
}