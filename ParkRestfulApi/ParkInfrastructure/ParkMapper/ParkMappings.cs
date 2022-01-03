using AutoMapper;
using ParkCore.Models;
using ParkCore.Models.Dtos;

namespace ParkInfrastructure.ParkMapper
{
    public class ParkMappings : Profile
    {
        public ParkMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}
