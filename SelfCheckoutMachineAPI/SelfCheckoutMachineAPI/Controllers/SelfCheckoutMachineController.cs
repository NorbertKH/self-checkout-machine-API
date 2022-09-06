using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachineAPI.Dtos;
using SelfCheckoutMachineAPI.Services;

namespace SelfCheckoutMachineAPI.Controllers
{
    /// <summary>
    /// Receives the API requests and handles messages
    /// </summary>
    [ApiController]
    public class SelfCheckoutMachine : ControllerBase
    {
        private readonly ICheckoutMachineService _checkoutMachineService;

        public SelfCheckoutMachine(ICheckoutMachineService checkoutMachineService)
        {
            _checkoutMachineService = checkoutMachineService;
        }

        /// <summary>
        /// The request that handels money stocking
        /// </summary>
        /// <param name="stock">The money that was put into the machine</param>
        /// <returns>OK (json) ot BadRequest</returns>
        [HttpPost("api/v1/Stock")]
        public IActionResult Stock(PaymentDto stock)
        {
            if (_checkoutMachineService.Stock(stock.Inserted))
            {
                return Ok(_checkoutMachineService.GetAvailableCurrency());
            }

            return BadRequest("Invalid currency given!");
        }

        /// <summary>
        /// Requset for the current amount of money in the machine
        /// </summary>
        /// <returns>OK (json)</returns>
        [HttpGet("api/v1/Stock")]
        public IActionResult Stock()
        { 
            return Ok(_checkoutMachineService.GetAvailableCurrency());
        }

        /// <summary>
        /// Request for the exchange
        /// </summary>
        /// <param name="checkOut"></param>
        /// <returns>OK (json) or BadRequest</returns>
        [HttpPost("/api/v1/Checkout")]
        public IActionResult CheckOut(PaymentDto checkOut)
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