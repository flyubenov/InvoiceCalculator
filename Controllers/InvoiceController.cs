using CsvHelper;
using InvoiceCalculator.Models;
using InvoiceCalculator.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculator.Controllers
{
    [Route("invoice")]
    public class InvoiceController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;

        public InvoiceController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("Error")]
        [HttpGet]
        public IActionResult HandleErrors()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(context.Error.Message);
        }

        [Route("save")]
        [HttpPost("save")]
        public IActionResult Save(InvoiceUploadModel model)
        {
            if (model.InvoiceData == null || model.InvoiceData.Length == 0)
            {
                return Content("File not selected");
            }
            else
            {
                var invoiceTotalAmount = new InvoiceCalculatorService().ProcessInvoices(model);              
                return View("Result", new InvoiceTotalAmountResultModel { InvoiceTotalAmount= invoiceTotalAmount, Currency = model.OutputCurrency});              
            }
        }

        

    }
}
