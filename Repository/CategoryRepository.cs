using Microsoft.EntityFrameworkCore.Diagnostics;
using Pokemons.Data;
using Pokemons.Interfaces;
using Pokemons.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pokemons.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int id)
        {
            return _context.Categories.Any(c => c.Id == id); 
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
           _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories() => _context.Categories.ToList();

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }
        

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(c=>c.CategoryId == categoryId).Select(c=>c.Pokemon).ToList();
        }

        public bool Save()
        {
           var save = _context.SaveChanges();
            return save>0?true:false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
             return Save();
        }
    }
}
