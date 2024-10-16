using Microsoft.AspNetCore.Mvc;
using OneStream.Domain.Interfaces;
using OneStream.infra.OpenWeather.FiltersApiSecurity;

namespace OneStream.Infra.OpenWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _openWeatherService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService openWeatherService)
        {
            _logger = logger;
            _openWeatherService = openWeatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string city, string state, string country)
        {
            _logger.LogInformation($"Get city weather for {city}, {state}, {country}");
            var result = await _openWeatherService.GetTheWeaterByCity($"{city},{state},{country}");
            if (result == null) 
            { 
                return BadRequest(); 
            }
            return Ok(result);
        }
    }
}
