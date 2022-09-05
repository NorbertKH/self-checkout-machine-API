using Microsoft.Extensions.Options;
using SelfCheckoutMachineAPI.Configurations;

namespace SelfCheckoutMachineAPI.Services
{
    public class CheckoutMachineService : ICheckoutMachineService
    {
        private Dictionary<string, int> _availableCurrency = new Dictionary<string, int>();
        private readonly CurrencyOptions _options;
        private readonly ILogger<CheckoutMachineService> _logger;

        public CheckoutMachineService(IOptions<CurrencyOptions> options, ILogger<CheckoutMachineService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public bool Stock(Dictionary<string, int> currency)
        {
            if (!IsCurrencyValid(currency))
            {
                return false;
            }
            foreach (var item in currency)
            {
                if (_availableCurrency.ContainsKey(item.Key))
                {
                    _availableCurrency[item.Key] += item.Value;
                }
                else
                {
                    _availableCurrency.Add(item.Key, item.Value);
                }
            }
            return true;
        }

        public Dictionary<string, int> GetAvailableCurrency() { return _availableCurrency; }

        private bool IsCurrencyValid(Dictionary<string, int> currency)
        {
            foreach (var item in currency)
            {
                if (!_options.ValidBills.Contains(item.Key))
                {
                    _logger.LogWarning("The given currency '{Currency}' is invalid!", item.Key);
                    return false;
                }
            }
            return true;
        }
    }
}
