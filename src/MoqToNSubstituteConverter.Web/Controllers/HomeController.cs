using Microsoft.AspNetCore.Mvc;
using MoqToNSubstituteConverter.Web.Models;
using System.Diagnostics;

namespace MoqToNSubstituteConverter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //        private readonly TelemetryClient _telemetry;

        public HomeController(ILogger<HomeController> logger)//, TelemetryClient telemetry)
        {
            _logger = logger;
            //           _telemetry = telemetry;
        }

        [HttpGet]
        [HttpHead]
        public IActionResult Index()
        {
            ConversionResponse gitHubResult = new();
            return View(viewName: "Index", model: (gitHubResult, false));
        }

        [HttpPost]
        public IActionResult Index(string code)
        {
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(model: result);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Example1()
        {
            string code = Examples.Example1();
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(viewName: "Index", model: result);
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
    }
}