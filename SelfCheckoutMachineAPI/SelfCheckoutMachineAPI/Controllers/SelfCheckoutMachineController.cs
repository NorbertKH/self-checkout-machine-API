using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachineAPI.Dtos;
using SelfCheckoutMachineAPI.Services;

namespace SelfCheckoutMachineAPI.Controllers
{
    [ApiController]
    public class SelfCheckoutMachine : ControllerBase
    {
        private readonly ICheckoutMachineService _checkoutMachineService;

        public SelfCheckoutMachine(ICheckoutMachineService checkoutMachineService)
        {
            _checkoutMachineService = checkoutMachineService;
        }

        [HttpPost("api/v1/Stock")]
        public async Task<IActionResult> Stock(Dictionary<string, int> stock)
        {
            if (_checkoutMachineService.Stock(stock))
            {
                return Ok(_checkoutMachineService.GetAvailableCurrency());
            }

            return BadRequest("Invalid currency given!");
        }

        [HttpGet("api/v1/Stock")]
        public async Task<IActionResult> Stock()
        { 
            return Ok();
        }

        [HttpGet("/api/v1/Checkout")]
        public async Task<IActionResult> CheckOut()
        {
            return Ok(nameof(CheckOut));
        }
    }
}