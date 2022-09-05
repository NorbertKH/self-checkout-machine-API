using Microsoft.AspNetCore.Mvc;

namespace SelfCheckoutMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelfCheckoutMachine : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PaymantPost()
        {
            return Ok();
        }
    }
}