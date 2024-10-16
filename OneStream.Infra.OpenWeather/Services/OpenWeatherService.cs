using Microsoft.Extensions.Options;
using OneStream.Domain.Entities;
using OneStream.Domain.Interfaces;
using OneStream.infra.OpenWeather.Entities;
using OneStream.Infra.OpenWeather.Entities;
using OneStream.Infra.OpenWeather.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OneStream.Infra.OpenWeather.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly AppConfig _config;
        private readonly ILogger<OpenWeatherService> _logger;
        private readonly HttpClient _httpClient;

        public OpenWeatherService(IOptions<AppConfig> config, ILogger<OpenWeatherService> logger, HttpClient httpClient)
        {
            _config = config.Value;
            _logger = logger;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(_config.BasePath);
        }
        /// <summary>
        /// Returns the city current weather 
        /// </summary>
        /// <param name="cityReference">The City Reference like "Apopka, FL, US" </param>
        /// <returns></returns>
        public async Task<WeatherRecord> GetTheWeaterByCity(string cityReference)
        {
            //api.openweathermap.org / data / 2.5 / weather ? q = Apopka,FL, US & APPID = 211e231c2b9f5ba1c89c64630490a89e
            var result = await _httpClient.GetFromJsonAsync<Root>($"{_httpClient.BaseAddress}2.5/weather?q={cityReference}&appid={_config.Key}");
            if (result == null)
            {
                return null;
            }
            return GetTheWeaterRecord(result);
        }

        private WeatherRecord GetTheWeaterRecord(Root value)
        {
            return new WeatherRecord
            {
                CityId = value.Id,
                CityName = value.Name,
                Date = value.Dt.FromUnixTime(),
                Lat = value.Coord.Lat,
                Lon = value.Coord.Lon,
                FeelsLike = value.Main.FeelsLike,
                Temp = value.Main.Temp,
                Pressure = value.Main.Pressure,
                Humidity = value.Main.Humidity,
                TempMin = value.Main.TempMin,
                TempMax = value.Main.TempMax,
                SeaLevel = value.Main.SeaLevel,
                GrndLevel = value.Main.GrndLevel,
                Sunrise = value.Sys.Sunrise.FromUnixTime(),
                Sunset = value.Sys.Sunset.FromUnixTime(),
                WindSpeed = value.Wind.Speed,
                WindDeg = value.Wind.Deg,
                WindGust = value.Wind.Gust,
                Id = Guid.NewGuid()
            };

        }

    }
}
