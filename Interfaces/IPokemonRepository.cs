using Pokemons.DTO;
using Pokemons.Models;

namespace Pokemons.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons();
        public Pokemon GetPokemonById(int id);
        public Pokemon GetPokemonByName(string name);
        public bool PokemonExist(int pokeId);
        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        public bool CreatePokemon (int ownerId,int categoryId,Pokemon pokemon);
        public bool UpdatePokemon (int ownerId,int categoryId,Pokemon pokemon);
        public bool DeletePokemon (Pokemon pokemon);
        public bool Save();
    }
}
