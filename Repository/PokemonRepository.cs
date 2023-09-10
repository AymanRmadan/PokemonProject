using Pokemons.Data;
using Pokemons.DTO;
using Pokemons.Interfaces;
using Pokemons.Models;

namespace Pokemons.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonownerentity=_context.Owners.Where(o=>o.Id== ownerId).FirstOrDefault(); 
            var category=_context.Categories.Where(c=>c.Id== categoryId).FirstOrDefault();
            var pokemonowner = new PokemonOwner()
            {
                Owner= pokemonownerentity,
                Pokemon= pokemon,
            };
            _context.Add(pokemonowner);

            var pokemoncategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };
            _context.Add(pokemoncategory);
            _context.Add(pokemon);
            return Save();

        }

        public Pokemon GetPokemonById(int id)
        {
            return _context.Pokemons.Where(d => d.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _context.Pokemons.Where(d => d.Name == name).FirstOrDefault();
        }

       

        public ICollection<Pokemon> GetPokemons()=> _context.Pokemons.OrderBy(p => p.Id).ToList();

        public bool PokemonExist(int pokeId)
        {
            return _context.Pokemons.Any(d=>d.Id == pokeId);
        }

        public bool Save()
        {
            var save= _context.SaveChanges();
            return save>0?true:false;
        }

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }
    }
}
