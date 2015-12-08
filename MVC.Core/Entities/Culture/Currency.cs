namespace MVC.Core.Entities.Culture
{
    using System.ComponentModel.DataAnnotations;

    public class Currency
	{
        [Required]
        public string Name { get; set; }
	}
}
