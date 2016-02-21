﻿namespace MVC.Core.Entities.Account
{
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity
    {
        [Required, MaxLength(256)]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }
    }
}
