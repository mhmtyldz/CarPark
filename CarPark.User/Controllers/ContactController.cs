using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.User.Localizer;
using CarPark.User.Models;
using CarPark.User.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CarPark.User.Controllers
{
    public class ContactController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public ContactController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }
        public IActionResult Index()
        {
            var controllerName = _sharedLocalizer["Hello"];
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactRequestModel request)
        {
            return View(request);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ContactAddRequestModel request)
        {
            return View(request);
        }
    }
}