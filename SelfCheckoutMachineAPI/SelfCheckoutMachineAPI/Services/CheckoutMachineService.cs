using Microsoft.Extensions.Options;
using SelfCheckoutMachineAPI.Configurations;
using SelfCheckoutMachineAPI.Dtos;
using SelfCheckoutMachineAPI.Exceptions;

namespace SelfCheckoutMachineAPI.Services
{
    /// <summary>
    /// Contains the functions and data to calculate money in the machine.
    /// </summary>
    public class CheckoutMachineService : ICheckoutMachineService
    {
        private Dictionary<string, int> _availableCurrency = new Dictionary<string, int>();
        private readonly CurrencyOptions _options;
        private readonly ILogger<CheckoutMachineService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">Contains the available bills</param>
        /// <param name="logger"></param>
        public CheckoutMachineService(IOptions<CurrencyOptions> options, ILogger<CheckoutMachineService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// Stock the machine with money
        /// </summary>
        /// <param name="currency">The money to stock into the machine</param>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Calculateing the exchange taking place
        /// </summary>
        /// <param name="payment">The bills payed by the customer</param>
        /// <returns>Returns the chage</returns>
        /// <exception cref="InvalidPaymentExcpetion">Thrwos different kind of exceptions depending on the payment</exception>
        public Dictionary<string, int> ExchangeCurrency(PaymentDto payment)
        {
            if (!IsCurrencyValid(payment.Inserted))
            {
                throw new InvalidPaymentExcpetion("Invalid currency!");
            }
            if (!IsPaymentValid(payment))
            {
                throw new InvalidPaymentExcpetion("Invalid payment (The money given is less than needed)!");
            }
            int? price = null;
            if (payment.Price == null)
            {
                throw new InvalidPaymentExcpetion("No price given!");
            }
            else
            {
                price = payment.Price;
            }

            foreach (var item in payment.Inserted)
            {
                price -= int.Parse(item.Key) * item.Value;
            }
            price = -price;
            return CalculateChange(price, payment.Inserted);
        }

        /// <summary>
        /// Calculates the change
        /// </summary>
        /// <param name="price">The remaining money to give the customer back</param>
        /// <param name="payment">The bills payed by customer</param>
        /// <returns></returns>
        /// <exception cref="InvalidPaymentExcpetion"></exception>
        private Dictionary<string, int> CalculateChange(int? price, Dictionary<string, int> payment)
        {
            var reverseBills = _options.ValidBills;
            reverseBills.Reverse();
            Dictionary<string, int> change = new Dictionary<string, int>();
            Dictionary<string, int> tempCurency = new Dictionary<string, int>(_availableCurrency);
            /*foreach (var item in reverseBills)
            {
                if (!tempCurency.ContainsKey(item))
                {
                    continue;
                }
                int temp = _availableCurrency
                for (int i = 0; i < _availableCurrency[item]; i++)
                {

                }
            }*/
            foreach (var item in reverseBills)
            {
                if (!tempCurency.ContainsKey(item))
                {
                    continue;
                }
                if (tempCurency[item] == 0)
                {
                    if (item == "5" && price >= 5)
                    {
                        foreach (var money in payment)
                        {
                            _availableCurrency[money.Key] -= money.Value;
                        }
                        throw new InvalidPaymentExcpetion("Not enought money in the machine or out of exact change!");
                    }
                    continue;
                }

                _logger.LogWarning("curr: {price}", tempCurency[item]);
                int numOfCurrency = tempCurency[item];
                _logger.LogWarning("curr: {price}", numOfCurrency);
                for (int i = 0; i <= _availableCurrency[item]; ++i)
                {
                    _logger.LogWarning("i: {price}", i);
                    _logger.LogWarning("s: {price}", tempCurency[item]);
                    if (price >= int.Parse(item))
                    {
                        price -= int.Parse(item);
                        tempCurency[item]--;
                        if (change.ContainsKey(item))
                        {
                            change[item] += 1;
                        }
                        else
                        {
                            change.Add(item, 1);
                        }
                        _logger.LogWarning("price: {price}", price);
                        _logger.LogWarning("remains: {price}", tempCurency);
                    }
                }
            }

            if (price >= 5)
            {
                foreach (var money in payment)
                {
                    _availableCurrency[money.Key] -= money.Value;
                }
                throw new InvalidPaymentExcpetion("Not enought money in the machine or out of exact change!");
            }

            foreach (var item in change)
            {
                _availableCurrency[item.Key] -= item.Value;
            }

            return change;
        }
        /// <summary>
        /// validates payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        private bool IsPaymentValid(PaymentDto payment)
        {
            int MoneyGiven = 0;
            foreach (var item in payment.Inserted)
            {
                MoneyGiven += item.Value * int.Parse(item.Key);
            }
            if (MoneyGiven >= payment.Price)
            {
                return true;
            }
            _logger.LogWarning("The payed amount is less than needed: (Payed, Needed: '{Payed}')",
                               payment.Inserted.Keys,
                               payment.Price);
            return false;
        }
        /// <summary>
        /// Validate currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
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
