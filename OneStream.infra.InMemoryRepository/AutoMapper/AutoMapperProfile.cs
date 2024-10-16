using AutoMapper;
using OneStream.Domain.Entities;
using OneStream.infra.InMemoryRepository.Data;


namespace OneStream.infra.InMemoryRepository.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<WeatherRecord, WeatherRecords>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src=>"Unknow User"))
                .ReverseMap();
                
        }
    }
}
