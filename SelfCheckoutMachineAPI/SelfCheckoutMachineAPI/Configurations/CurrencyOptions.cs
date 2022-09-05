namespace SelfCheckoutMachineAPI.Configurations
{
    public class CurrencyOptions
    {
        public const string SectionName = "CurrencyOptions";
        public List<string> ValidBills { get; set; } = new();
    }
}
