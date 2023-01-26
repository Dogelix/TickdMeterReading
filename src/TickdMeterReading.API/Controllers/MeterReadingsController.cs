using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TickdMeterReading.Application.Services;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.MeterReadings;
using Serilog;

namespace TickdMeterReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingsController( IMeterReadingService meterReadingService ) 
        { 
            _meterReadingService = meterReadingService;
        }

        /// <summary>
        /// Add new meter readings
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MeterReadingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] IFormFile csv)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}
