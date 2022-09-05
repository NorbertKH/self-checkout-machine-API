using SelfCheckoutMachineAPI.Dtos;

namespace SelfCheckoutMachineAPI.Services
{
    public interface ICheckoutMachineService
    {
        Dictionary<string, int> ExchangeCurrency(PaymentDto payment);
        Dictionary<string, int> GetAvailableCurrency();
        bool Stock(Dictionary<string, int> currency);
    }
}