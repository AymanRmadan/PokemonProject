using Pokemons.Data;
using Pokemons.Interfaces;
using Pokemons.Models;
using System.Reflection.Metadata.Ecma335;

namespace Pokemons.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CraeteReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReview(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReviewById(int reviewId) =>
            _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();


        public ICollection<Review> GetReviewOfPokemon(int pokemonId)
        {
           return _context.Reviews.Where(p=>p.Pokemon.Id== pokemonId).ToList();
        }

        public ICollection<Review> GetReviews()=> _context.Reviews.ToList();      

        public bool ReviewExist(int reviewId)=> _context.Reviewers.Any(r=>r.Id == reviewId);

        public bool Save()
        {
           var save= _context.SaveChanges();
            return save>0?true: false;
        }
    }
}
