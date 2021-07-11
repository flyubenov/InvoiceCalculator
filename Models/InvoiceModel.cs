using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCalculator.Models
{
    public class InvoiceModel
    {
        public string Customer { get; set; }
        [Name("Vat number")]
        public string Vat { get; set; }
        [Name("Document number")]
        public string DocumentNumber { get; set; }
        public InvoiceType Type { get; set; }
        [Name("Parent document")]
        public string ParentDocument { get; set; }
        public Currency Currency { get; set; }
        public decimal Total { get; set; }
    }
}
