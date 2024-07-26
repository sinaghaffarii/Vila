using AutoMapper;
using Vila.WebApi.Dtos;

namespace Vila.WebApi.Mappings
{
    public class ModelsMapper : Profile

    {
        public ModelsMapper()
        {
            CreateMap<Models.Vila, VilaDto>()
                .ForMember(x => x.Shahr, d => d.MapFrom(des => des.City))
                .ReverseMap()
                .ForMember(x => x.City, d => d.MapFrom(des => des.Shahr));

            CreateMap<Models.Detail, DetailDto>().ReverseMap();
        }
    }
}
