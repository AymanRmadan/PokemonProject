using AutoMapper;
using Pokemons.DTO;
using Pokemons.Models;

namespace Pokemons.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon,PokemonDto>().ReverseMap();
           // CreateMap<PokemonDto,Pokemon>();
            CreateMap<Category,CategoryDto>().ReverseMap();
          //  CreateMap<CategoryDto,Category>();
            CreateMap<Country,CountryDto>().ReverseMap();
           // CreateMap<CountryDto,Country>();
            CreateMap<Owner,OwnerDto>().ReverseMap();
            CreateMap<Review,ReviewsDto>();
           // CreateMap<ReviewsDto,Review>();
            CreateMap<Reviewer,ReviewerDto>().ReverseMap();
           // CreateMap<ReviewerDto,Reviewer>();
        }
    }
}
