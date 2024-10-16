using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStream.Domain.Entities;
using OneStream.infra.InMemoryRepository.Data;
using OneStream.infra.InMemoryRepository.FiltersApiSecurity;
using OneStream.infra.InMemoryRepository.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OneStream.infra.InMemoryRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    [ApiKey]
    public class WeatherRecordController : ControllerBase
    {
        private IWeatherRecordServices _weatherRecordServices;

        public WeatherRecordController(IWeatherRecordServices service)
        {
           _weatherRecordServices = service;
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
