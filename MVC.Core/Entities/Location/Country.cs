namespace MVC.Core.Entities.Location
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using MVC.Core.Entities.Culture;
	
	public class Country : BaseEntity
    {
		[Required, MaxLength(50)]
		public string Name { get; set; }

		[Index(IsUnique = true), Required, RegularExpression("^[A-Z]{2}$")]
		public string IsoCode2A { get; set; }

		[Index(IsUnique = true), Required, RegularExpression("^[A-Z]{3}$")]
		public string IsoCode3A { get; set; }

		[Required, RegularExpression("^[0-9]{3}$")]
		public string IsoCode3N { get; set; }

		public string DiallingCode { get; set; }

		public Currency Currency { get; set; }
    }
}
