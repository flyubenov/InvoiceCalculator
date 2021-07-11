using CsvHelper;
using InvoiceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCalculator.Services
{
    public class InvoiceCalculatorService
    {
        public decimal ProcessInvoices(InvoiceUploadModel model)
        {
            var exchangeRates = GetExchangeRates(model);
            var invoices = ParseInvoiceData(model);
            
            var invoiceTotalAmount = SumInvoiceTotals(invoices, exchangeRates, model.OutputCurrency);

            return invoiceTotalAmount;
        }

        private List<ExchangeRate> GetExchangeRates(InvoiceUploadModel model)
        {
            var result = new List<ExchangeRate>();
            var rates = model.ListOfExchangeRates.Split(",").ToList();
            foreach (var r in rates)
            {
                Enum.TryParse(r.Split(":")[0], out Currency currency);
                decimal.TryParse(r.Split(":")[1], out decimal rate);
                result.Add(new ExchangeRate { Currency = currency, RateAgainstOutputCurrency = rate });
            }
            return result;
        }

        private List<InvoiceModel> ParseInvoiceData(InvoiceUploadModel model)
        {
            var csvReader = new CsvReader(new StreamReader(model.InvoiceData.OpenReadStream()), CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<InvoiceModel>().ToList();
            return records;
        }

        private decimal SumInvoiceTotals(List<InvoiceModel> records, List<ExchangeRate> rates, Currency outputCurrency)
        {
            decimal result = 0;
            foreach (var invoice in records)
            {
                if (invoice.Currency == outputCurrency)
                    result += invoice.Total;
                else
                {
                    var rate = rates.FirstOrDefault(r => r.Currency == invoice.Currency);
                    if (rate == null)
                        throw new ArgumentException($"Exchange rate against output currency is missing for invoice number {invoice.DocumentNumber}, invoice currency {invoice.Currency} ");

                    result += invoice.Total * rate.RateAgainstOutputCurrency;
                }
            }
            return result;
        }        
    }
}
