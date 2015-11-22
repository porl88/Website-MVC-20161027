namespace MVC.Core.Entities.Location
{
    public class Address : BaseEntity
    {
		public string Number { get; set; }

		public string Street { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }
		
		public string TownCity { get; set; }

		public string County { get; set; }
		
		public string PostCode { get; set; }

        public Country Country { get; set; }
    }
}
