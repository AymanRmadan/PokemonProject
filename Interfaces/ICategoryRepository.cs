using Pokemons.Models;
using System.Collections.ObjectModel;

namespace Pokemons.Interfaces
{
    public interface ICategoryRepository
    {
        public ICollection<Category> GetCategories();
        public Category GetCategoryById(int id);
        public bool CategoryExist(int id);
        public ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        public bool CreateCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
        public bool Save();
    }

}
