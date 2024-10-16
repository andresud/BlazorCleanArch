using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStream.Domain.Entities;
using OneStream.Domain.Services;
using OneStream.infra.InMemoryRepository.Services;

namespace OneStream.infra.InMemoryRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherRecordController : ControllerBase
    {
        private IWeatherRecordServices _weatherRecordServices;
        private readonly ILogger<WeatherRecordController> _logger;

        public WeatherRecordController(IWeatherRecordServices service, ILogger<WeatherRecordController> logger)
        {
            _weatherRecordServices = service;
            _logger = logger;
        }

        // GET: api/<WeatherRecordController>
        [HttpGet]        
        public async Task<IActionResult> GetAsync()
        {
            var result = await _weatherRecordServices.GetAllRecords();
            return Ok(result);
        }

        // GET api/<WeatherRecordController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result =  await _weatherRecordServices.GetRecordById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
            
        }

        // POST api/<WeatherRecordController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherRecord record)
        {
            try
            {
                var result = await _weatherRecordServices.Create(record);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<WeatherRecordController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WeatherRecord record)
        {
            try
            {
                var result = await _weatherRecordServices.Update(record);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<WeatherRecordController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _weatherRecordServices.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
