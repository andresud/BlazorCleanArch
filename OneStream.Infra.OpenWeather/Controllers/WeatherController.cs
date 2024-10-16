using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStream.Domain.Interfaces;


namespace OneStream.Infra.OpenWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var result = await _openWeatherService.GetTheWeaterByCity($"{city}, {state}, {country}");
            if (result == null) 
            { 
                return BadRequest(); 
            }
            return Ok(result);
        }
    }
}
