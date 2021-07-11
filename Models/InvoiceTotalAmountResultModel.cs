using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCalculator.Models
{
    public class InvoiceTotalAmountResultModel
    {
        public decimal InvoiceTotalAmount { get; set; }
        public Currency Currency { get; set; }
    }
}
