using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCalculator.Models
{
    public class InvoiceUploadModel
    {
        public Currency OutputCurrency { get; set; }
        public IFormFile InvoiceData { get; set; }
        public string ListOfExchangeRates { get; set; }
    }
}
