using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OneStream.Domain.Entities;
using OneStream.Domain.Services;
using OneStream.infra.InMemoryRepository.Data;

namespace OneStream.infra.InMemoryRepository.Services
{
    public class WeatherRecordService : IWeatherRecordServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WeatherRecordService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<WeatherRecord> Create(WeatherRecord record)
        {
            record.Id = Guid.NewGuid();
            WeatherRecords weaterRecord = _mapper.Map<WeatherRecords>(record);
            _context.Add(weaterRecord);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<WeatherRecord> Delete(Guid id)
        {
            var weatherRecord = await _context.WeatherRecords.FindAsync(id);
            if (weatherRecord != null)
            {
                _context.WeatherRecords.Remove(weatherRecord);
                await _context.SaveChangesAsync();

                return _mapper.Map<WeatherRecord>(weatherRecord);
            }
            else
            {
                throw new KeyNotFoundException($"This Weather Record was not found id: {id}");
            }
        }

        public async Task<List<WeatherRecord>> GetAllRecords()
        {
            return _mapper.Map<List<WeatherRecords>, List<WeatherRecord>>(await _context.WeatherRecords.ToListAsync());
        }

        public Task<List<WeatherRecord>> GetAllRecordsForCity(string city)
        {
            throw new NotImplementedException();
        }

        public async Task<WeatherRecord> GetRecordById(Guid id)
        {
            var weatherRecord = await _context.WeatherRecords.FindAsync(id);
            if (weatherRecord == null)
            {
                throw new KeyNotFoundException($"This Weather Record was not found id: {id}");
            }
            return _mapper.Map<WeatherRecords, WeatherRecord>(weatherRecord);
        }

        public async Task<WeatherRecord> Update(WeatherRecord record)
        {
            try
            {
                var weaterRecord = _mapper.Map<WeatherRecord, WeatherRecords>(record);
                _context.Update(weaterRecord);
                await _context.SaveChangesAsync();
                return record;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WeatherRecordExists(record.Id))
                {
                    throw new KeyNotFoundException($"This Weather Record was not found id: {record.Id}");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> WeatherRecordExists(Guid id)
        {
            return await _context.WeatherRecords.AnyAsync(e => e.Id == id);
        }
    }
}
