using Pokemons.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace Pokemons.Interfaces
{
    public interface ICountryRepository
    {
        public ICollection<Country> GetCountries();
        public Country GetCountryById(int id);
        public bool CountryExist(int id);
        public ICollection<Owner> GetOnwersFromACountry(int countryid);
        public Country GetCountryByOwner(int ownerid);

        public bool UpdateCountry(Country country);
        public bool DeleteCountry(Country country);
        public bool Save();


    }
}
