using Pokemons.Models;

namespace Pokemons.Interfaces
{
    public interface IReviewRepository
    {
        public ICollection<Review> GetReviews();
        public Review GetReviewById(int reviewId);
        public ICollection<Review> GetReviewOfPokemon(int pokemonId);
        public bool ReviewExist(int reviewId);
        public bool CraeteReview(Review review);
        public bool DeleteReview(Review review);
        public bool Save();
        bool DeleteReview(List<Review> reviews);
    }
    
}
