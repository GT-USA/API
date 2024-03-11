using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //Villa Mapping
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            //ReverseMap() is for avoid typing like upper example
            CreateMap<Villa, VillaDTOCreate>().ReverseMap();
            CreateMap<Villa, VillaDTOUpdate>().ReverseMap();

            //VillaNumber Mapping
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOCreate>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOUpdate>().ReverseMap();
        }
    }
}
