using Newtonsoft.Json;

namespace SelfCheckoutMachineAPI.Dtos
{
    public class StockDto
    {
        public Dictionary<string, int> Inserted { get; set; } = new Dictionary<string, int>();
    }
}
