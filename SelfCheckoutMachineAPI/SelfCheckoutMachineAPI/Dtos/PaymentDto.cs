using Newtonsoft.Json;

namespace SelfCheckoutMachineAPI.Dtos
{
    public class PaymentDto
    {
        public Dictionary<string, int> Inserted { get; set; } = new Dictionary<string, int>();

        public int? Price { get; set; }
    }
}
