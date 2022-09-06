namespace SelfCheckoutMachineAPI.Configurations
{
    /// <summary>
    /// Contains the valid bills for validation and the name helping to load it in from json
    /// </summary>
    public class CurrencyOptions
    {
        public const string SectionName = "CurrencyOptions";
        public List<string> ValidBills { get; set; } = new();
    }
}
