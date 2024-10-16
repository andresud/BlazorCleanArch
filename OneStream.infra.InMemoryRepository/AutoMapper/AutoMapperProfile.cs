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
                .ReverseMap();
        }
    }
}
