using Pokemons.Models;

namespace Pokemons.Interfaces
{
    public interface IReviewerRepository
    {
        public ICollection<Reviewer> GetReviewers();
        public Reviewer GrtReviewerById(int reviewerId);
        public ICollection<Review> GetReviewsByReviewer(int reviewerId);
        public bool ReviewerExist(int reviewerId);
        public bool CreateReiewer(Reviewer reviewer);
        public bool DeleteReviewer(Reviewer reviewer);
        public bool Save();
      
    }
}
