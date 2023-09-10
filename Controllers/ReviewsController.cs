using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemons.DTO;
using Pokemons.Interfaces;
using Pokemons.Models;
using Pokemons.Repository;

namespace Pokemons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewsController(IReviewRepository reviewRepository
            ,IMapper mapper,
            IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository
            )
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetCategories()
        {
            var reviews= _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }

        [HttpGet("{reviewsId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int reviewsId)
        {
            if (!_reviewRepository.ReviewExist(reviewsId))
                return NotFound();
            var review = _mapper.Map<ReviewsDto>(_reviewRepository.GetReviewById(reviewsId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }



        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAPokemon(int pokeId)
        {
            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviewOfPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery]int revwerId, [FromQuery]int pokemonId, [FromBody]ReviewsDto reviewcreate)
        {
            if(reviewcreate is null)
                return BadRequest(ModelState);
            var reviews=_reviewRepository.GetReviews()
                .Where(c=>c.Title.Trim().ToUpper()== reviewcreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
            if(reviews!=null)
            {
                ModelState.AddModelError("", "Review is already exist");
                return StatusCode(422,ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewcreate);
            reviewMap.Pokemon = _pokemonRepository.GetPokemonById(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GrtReviewerById(revwerId);

            if(!_reviewRepository.CraeteReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("reveiwId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reveiwId)
        {
            if (!_reviewRepository.ReviewExist(reveiwId)) return NotFound(ModelState);
            var reviewDeleted = _reviewRepository.GetReviewById(reveiwId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!_reviewRepository.DeleteReview(reviewDeleted))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
