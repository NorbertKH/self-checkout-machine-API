﻿using Newtonsoft.Json;

namespace SelfCheckoutMachineAPI.Dtos
{
    /// <summary>
    /// The DTO that contains the inserted bills and the price if it is given
    /// </summary>
    public class PaymentDto
    {
        public Dictionary<string, int> Inserted { get; set; } = new Dictionary<string, int>();

        public int? Price { get; set; }
    }
}
