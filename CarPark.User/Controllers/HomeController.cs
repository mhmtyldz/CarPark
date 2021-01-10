using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarPark.User.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading;

namespace CarPark.User.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger,
            IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var helloKarsiligi = _localizer["Hello"];

            //Dili değiştiriyorum burdaki kod ile ingilizce yaptım
            //var cultureInfo = CultureInfo.GetCultureInfo("en-US");

            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;


            //var helloKarsiligi2 = _localizer["Hello"];
            ////Daha sonra arapça yaptım
            //var cultureInfo2 = CultureInfo.GetCultureInfo("ar-SA");

            //Thread.CurrentThread.CurrentCulture = cultureInfo2;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo2;

            //var helloKarsiligi3 = _localizer["Hello"];


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

        public IActionResult List()
        {
            return View();
        }
    }
}
