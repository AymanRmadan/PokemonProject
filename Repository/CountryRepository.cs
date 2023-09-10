using Microsoft.EntityFrameworkCore.Diagnostics;
using Pokemons.Data;
using Pokemons.Interfaces;
using Pokemons.Models;
using System.Reflection.Metadata.Ecma335;

namespace Pokemons.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CountryExist(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool DeleteCountry(Country country)
        {
           _context.Countries.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.Where(c=>c.Id==id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerid)
        {
            return _context.Owners.Where(o=>o.Id== ownerid).Select(c=>c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOnwersFromACountry(int countryid)
        {
            return _context.Owners.Where(c => c.Country.Id == countryid).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
