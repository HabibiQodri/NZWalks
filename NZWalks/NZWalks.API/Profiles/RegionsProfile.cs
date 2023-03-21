using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile() {
            CreateMap<Region, RegionDto>()
                .ReverseMap();

                // Ketika ingin mengambalikan propety dto ke seperti sumbernya 
                // .ReverseMap();
                
                // ketika properti tidak memeliki nama yg sama antara sumber dan tujuan mapper
                // .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
        }
    }
}
