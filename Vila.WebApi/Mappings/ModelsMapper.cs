using AutoMapper;
using Vila.WebApi.Dtos;

namespace Vila.WebApi.Mappings
{
    public class ModelsMapper : Profile

    {
        public ModelsMapper()
        {
            CreateMap<Model.Vila, VilaDto>()
                .ForMember(x => x.Shahr, d => d.MapFrom(des => des.City));
        }
    }
}
