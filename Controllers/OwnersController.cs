using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemons.DTO;
using Pokemons.Interfaces;
using Pokemons.Models;
using System.Reflection.Metadata.Ecma335;

namespace Pokemons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerRepository ownerRepository,IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(owners);
        }

        [HttpGet("{onwerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerById(int onwerId)
        {
            if (!_ownerRepository.OwnersExist(onwerId))
                return NotFound();
            var owner = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerById(onwerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(owner);
        }

        [HttpGet("{onwerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOnwer(int onwerId)
        {
            if (!_ownerRepository.OwnersExist(onwerId))
                return NotFound();
            var pokemons=_mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsByOwner(onwerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemons);


        }


        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnersExist(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = _ownerRepository.GetOwnerById(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

    }
}
