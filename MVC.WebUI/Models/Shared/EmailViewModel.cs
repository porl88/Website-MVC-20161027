namespace MVC.WebUI.Models.Shared
{
	using System.ComponentModel.DataAnnotations;
    using Core.Configuration;

	public class EmailViewModel
	{
		private string firstName;

		private string lastName;

		private string email;

		private string subject;

        public string EmailPattern { get; set; }

		[Display(Name = "Your First Name")]
		[Required, MaxLength(30), RegularExpression(RegularExpressions.Name, ErrorMessage = "'{0}' must be a valid name. (Numbers and special characters not allowed).")]
		public string FirstName
		{
			get
			{
				return this.firstName;
			}

			set
			{
				this.firstName = value.Trim();
			}
		}

		[Display(Name = "Your Last Name")]
		[Required, MaxLength(30), RegularExpression(RegularExpressions.Email, ErrorMessage = "'{0}' must be a valid name.")]
		public string LastName
		{
			get
			{
				return this.lastName;
			}

			set
			{
				this.lastName = value.Trim();
			}
		}

		[Display(Name = "Your Email")]
		[Required, MaxLength(256), RegularExpression(RegularExpressions.Email, ErrorMessage = "'{0}' must be a valid email address.")]
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

		[Required, MaxLength(100)]
		public string Subject
		{
			get
			{
				return this.subject;
			}

			set
			{
				this.subject = value.Trim();
			}
		}

		[Required, MaxLength(2000)]
		public string Message { get; set; }
	}
}