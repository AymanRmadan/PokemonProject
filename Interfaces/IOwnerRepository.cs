using Pokemons.Models;

namespace Pokemons.Interfaces
{
    public interface IOwnerRepository
    {
        public ICollection<Owner> GetOwners();
        public Owner GetOwnerById(int ownerId);
        public ICollection<Pokemon> GetPokemonsByOwner(int ownreId);
        public ICollection<Owner> GetOwnersByPokemon(int pokemonId);
        public bool DeleteOwner(Owner owner);
        public bool OwnersExist(int ownerId);
        public bool Save();

    }
}
