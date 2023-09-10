using Microsoft.EntityFrameworkCore;
using Pokemons.Data;
using Pokemons.Interfaces;
using Pokemons.Models;

namespace Pokemons.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReiewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();

        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
           _context.Remove(reviewer);
            return Save();
        }

        public ICollection<Reviewer> GetReviewers()=>
            _context.Reviewers.ToList();
        

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)=>
            _context.Reviews.Where(r=>r.Reviewer.Id==reviewerId).ToList();
        

        public Reviewer GrtReviewerById(int reviewerId) =>
            _context.Reviewers.Where(r => r.Id == reviewerId).Include(r=>r.Reviews).FirstOrDefault();
      

        public bool ReviewerExist(int reviewerId)=>
            _context.Reviewers.Any(r=>r.Id== reviewerId);

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save>0?true:false;
        }
    }
}
