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
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public PokemonsController(IPokemonRepository pokemonRepository
            ,IReviewRepository reviewRepository
            ,IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons() {
            var pokemon = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonById(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound();
            var pokemon= _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemonById(pokeId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery]int owenrId, [FromQuery]int categoryId,[FromBody]PokemonDto pokemoncreate)
        {
            if (pokemoncreate is null)
                return BadRequest(ModelState);
            var pokemons = _pokemonRepository.GetPokemonTrimToUpper(pokemoncreate);

            if (pokemons is not null)
            {
                ModelState.AddModelError("", "Owner is already exist");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var pokemnomap = _mapper.Map<Pokemon>(pokemoncreate);

            if(!_pokemonRepository.CreatePokemon(owenrId,categoryId, pokemnomap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500,ModelState);
            }

            return Ok("Successfully Created");

        }
        
        [HttpPut("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId,[FromQuery]int ownerId,[FromQuery]int categoryId, [FromBody] PokemonDto updatedpokemon)
        {
            if (updatedpokemon == null)
                return BadRequest(ModelState);
            if (pokemonId != updatedpokemon.Id) return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            var pokemonMap = _mapper.Map<Pokemon>(updatedpokemon);
            if (!_pokemonRepository.UpdatePokemon(ownerId,categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if(!_pokemonRepository.PokemonExist(pokeId))
                return BadRequest(ModelState);

            var reviewdeleted = _reviewRepository.GetReviewOfPokemon(pokeId);
            var pokemondeleted = _pokemonRepository.GetPokemonById(pokeId);
            if (!_reviewRepository.DeleteReview(reviewdeleted.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }
            if (!_pokemonRepository.DeletePokemon(pokemondeleted))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }
            return NoContent();
        }


    }
}
