namespace MVC.Services.Location
{
    using System.Collections.Generic;
	using MVC.Core.Entities.Location;

    public interface ICountryService
    {
        Country GetCountry(string countryCode);

        IEnumerable<Country> GetCountries();
    }
}
