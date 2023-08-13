﻿using Microsoft.AspNetCore.Mvc;
using MoqToNSubstituteConverter.Web.Models;
using System.Diagnostics;

namespace MoqToNSubstituteConverter.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //        private readonly TelemetryClient _telemetry;

        public HomeController()//ILogger<HomeController> logger)//, TelemetryClient telemetry)
        {
            //_logger = logger;
            //           _telemetry = telemetry;
        }

        [HttpGet]
        [HttpHead]
        public IActionResult Index()
        {
            ConversionResponse result = new();
            return View(viewName: "Index", model: result);
        }

        [HttpPost]
        public IActionResult Index(string txtMoqCode)
        {
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(txtMoqCode);
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

        [HttpGet]
        [HttpPost]
        public IActionResult SimpleExample1()
        {
            string code = Examples.SimpleExample1();
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(viewName: "Index", model: result);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SimpleExample2()
        {
            string code = Examples.SimpleExample2();
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(viewName: "Index", model: result);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SimpleExample3()
        {
            string code = Examples.SimpleExample3();
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(viewName: "Index", model: result);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SimpleExample4()
        {
            string code = Examples.SimpleExample4();
            Conversion conversion = new();
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);
            return View(viewName: "Index", model: result);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SimpleExample5()
        {
            string code = Examples.SimpleExample5();
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