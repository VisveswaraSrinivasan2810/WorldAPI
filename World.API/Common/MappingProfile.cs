using AutoMapper;
using World.API.DTO;
using World.API.DTO.States;
using World.API.Models;

namespace World.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country,CountryDTO>().ReverseMap();
            CreateMap<Country,CountryUpdateDTO>().ReverseMap();
            CreateMap<State,StateDTO>().ReverseMap();
            CreateMap<State,CreateStateDTO>().ReverseMap();
            CreateMap<State,UpdateStateDTO>().ReverseMap();
        }
    }
}
