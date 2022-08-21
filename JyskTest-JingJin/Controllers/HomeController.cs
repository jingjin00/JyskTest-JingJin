using JyskTest_JingJin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RomanNumberConverter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JyskTest_JingJin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INumberConverter _romamNumberConverter;

        public HomeController(ILogger<HomeController> logger, INumberConverter romamNumberConverter)
        {
            _logger = logger;
            _romamNumberConverter = romamNumberConverter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Input a roman number and conver it to integer
        /// </summary>
        /// <param name="number">A string which is roman number format</param>
        /// <returns>A int to string and error message if any</returns>
        public IActionResult RomanToInt(string number)
        {
            (int value, string error) = _romamNumberConverter.RomanToInt(number);
            return new JsonResult(new ConverterResult { Text=value.ToString(), Error=error});
        }
        /// <summary>
        /// Input a decimal number and conver it to Roman
        /// </summary>
        /// <param name="number">A string which is decimal number format</param>
        /// <returns>Roman number and a error message if there is any</returns>
        public IActionResult IntToRoman(string number)
        {
            Regex regex = new Regex(@"^\d+$");

            if (!regex.IsMatch(number))
            {
                return new JsonResult(new ConverterResult { Text = String.Empty, Error = "Invalid number" });
            }
                (string text,string error) = _romamNumberConverter.IntToRoman(Int32.Parse(number));

            return new JsonResult(new ConverterResult { Text = text, Error = error });
        }
    }
    public class ConverterResult
    {
        public string Text { get; set; }
        public string Error { get; set; }
    }
}
