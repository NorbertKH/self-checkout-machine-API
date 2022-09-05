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
        public async Task<IActionResult> Stock(PaymentDto stock)
        {
            if (_checkoutMachineService.Stock(stock.Inserted))
            {
                return Ok(_checkoutMachineService.GetAvailableCurrency());
            }

            return BadRequest("Invalid currency given!");
        }

        [HttpGet("api/v1/Stock")]
        public async Task<IActionResult> Stock()
        { 
            return Ok(_checkoutMachineService.GetAvailableCurrency());
        }

        [HttpPost("/api/v1/Checkout")]
        public async Task<IActionResult> CheckOut(PaymentDto checkOut)
        {
            Dictionary<string, int> change = new Dictionary<string, int>();
            if (!_checkoutMachineService.Stock(checkOut.Inserted))
            {
                return BadRequest("Invalid currency given!");
            }
            try
            {
                change = _checkoutMachineService.ExchangeCurrency(checkOut);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(change);
        }
    }
}