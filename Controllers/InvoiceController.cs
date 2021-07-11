using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [Route("save")]
        [HttpPost("save")]
        public IActionResult Save(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected");
            }
            else
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }
                var a = result.ToString();
                return Content(a);



                //var stream = new MemoryStream();
                //photo.CopyToAsync(stream);
                //StreamReader reader = new StreamReader(stream);
                //string text = reader.ReadToEnd();
            }

            return View("Success");
        }

    }
}
