namespace SelfCheckoutMachineAPI.Services
{
    public interface ICheckoutMachineService
    {
        Dictionary<string, int> GetAvailableCurrency();
        bool Stock(Dictionary<string, int> currency);
    }
}