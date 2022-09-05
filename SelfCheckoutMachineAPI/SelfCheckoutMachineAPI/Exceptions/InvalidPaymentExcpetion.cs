namespace SelfCheckoutMachineAPI.Exceptions
{
    [Serializable]
    public class InvalidPaymentExcpetion : Exception
    {
        public InvalidPaymentExcpetion() : base() { }
        public InvalidPaymentExcpetion(string message) : base(message) { }
        public InvalidPaymentExcpetion(string message, Exception inner) : base(message, inner) { }
    }
}
