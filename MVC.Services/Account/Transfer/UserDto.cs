namespace MVC.Services.Account.Transfer
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [Required, MaxLength(30)]
        public string UserName { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
