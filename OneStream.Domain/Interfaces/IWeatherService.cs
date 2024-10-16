
using OneStream.Domain.Entities;

namespace OneStream.Domain.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherRecord> GetTheWeaterByCity(string cityReference);
    }
}