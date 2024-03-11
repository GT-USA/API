using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //Villa Mapping is changed with VillaDTO
            CreateMap<VillaDTO, VillaDTOCreate>().ReverseMap();
            //ReverseMap() is for avoid typing like upper example
            CreateMap<VillaDTO, VillaDTOUpdate>().ReverseMap();

            //VillaNumber Mapping
            CreateMap<VillaNumberDTO, VillaNumberDTOCreate>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberDTOUpdate>().ReverseMap();
        }
    }
}
