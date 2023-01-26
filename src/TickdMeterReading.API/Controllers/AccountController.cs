using System;
using System.Threading.Tasks;
using TickdMeterReading.Application.Services;
using TickdMeterReading.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TickdMeterReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;            
        }

        /// <summary>
        /// Get Accounts
        /// </summary>
        /// <returns>Returns a list of All Accounts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _accountService.GetAll());
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Get Account by ID
        /// </summary>
        /// <param name="id">Account's ID</param>
        /// <returns>Returns a Account by its ID</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _accountService.GetById(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new Account
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] AccountViewModel accountViewModel)
        {
            try
            {
                return Ok(await _accountService.Create(accountViewModel));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Delete an Account
        /// </summary>
        /// <param name="id">Account's ID</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _accountService.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}
