

using OneStream.Domain.Entities;

namespace OneStream.infra.InMemoryRepository.Services
{
    public interface IWeatherRecordServices
    {
        Task<List<WeatherRecord>> GetAllRecords();
        
        Task<List<WeatherRecord>> GetAllRecordsForCity(string city);

        Task<WeatherRecord> GetRecordById(Guid id);

        Task<WeatherRecord> Create(WeatherRecord record);

        Task<WeatherRecord> Update(WeatherRecord record);

        Task<WeatherRecord> Delete(Guid id);

        Task<bool> WeatherRecordExists(Guid id);
    }
}
