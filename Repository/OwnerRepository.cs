using Microsoft.EntityFrameworkCore.Diagnostics;
using Pokemons.Data;
using Pokemons.Interfaces;
using Pokemons.Models;
using System.Reflection.Metadata.Ecma335;

namespace Pokemons.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Owners.Remove(owner);
            return Save();
        }

        public Owner GetOwnerById(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Owner> GetOwnersByPokemon(int pokemonId)
        {
            return _context.PokemonOwners.Where(p=>p.Pokemon.Id==pokemonId).Select(o=>o.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownreId)
        {
            return _context.PokemonOwners.Where(o=>o.Owner.Id==ownreId).Select(p=>p.Pokemon).ToList();
        }

        public bool OwnersExist(int ownerId)
        {
            return _context.Owners.Any(o=>o.Id== ownerId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0? true : false;
        }
    }
}
