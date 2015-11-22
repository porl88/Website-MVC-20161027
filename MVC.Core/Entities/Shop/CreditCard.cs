namespace MVC.Core.Entities.Shop
{
	using System;

	public class CreditCard
	{
		CreditCardType Type { get; set; }

		string CardHolder { get; set; }

		string Number { get; set; }

		string SecurityCode { get; set; }

		DateTimeOffset Expires { get; set; }
	}
}
