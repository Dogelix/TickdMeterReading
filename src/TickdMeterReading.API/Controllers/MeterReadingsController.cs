using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TickdMeterReading.Application.Services;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.MeterReadings;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using TickdMeterReading.API.Extensions;
using System.Linq;

namespace TickdMeterReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly IWebHostEnvironment _environment;
        private readonly IAccountService _accountService;

        public MeterReadingsController( IMeterReadingService meterReadingService, IAccountService accountService, IWebHostEnvironment environment) 
        { 
            _meterReadingService = meterReadingService;
            _environment = environment;
            _accountService = accountService;
        }

        /// <summary>
        /// Add new meter readings
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MeterReadingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(IFormFile csv)
        {
            try
            {
                var allAccounts = await _accountService.GetAll();

                var validatedMeterReadings = await CsvValidator.ValidateCsvUploadAsync(_environment, csv, allAccounts.ToList());

                if(validatedMeterReadings == null)
                {
                    return BadRequest();
                }

                foreach (var reading in validatedMeterReadings)
                {
                    await _meterReadingService.Create(reading);
                }

                return Ok($"{validatedMeterReadings.Count} meter readings added.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}
