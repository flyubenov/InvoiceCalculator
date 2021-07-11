using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCalculator.Models
{
    public class ExchangeRate
    {
        public Currency Currency { get; set; }
        public decimal RateAgainstOutputCurrency { get; set; }
    }
}
