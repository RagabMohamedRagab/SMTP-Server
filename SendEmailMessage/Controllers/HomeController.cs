using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendEmail.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SendEmail.Utitles;
using SendEmail.ViewsModel;

namespace SendEmail.Controllers {
    public class HomeController : Controller {
        private readonly ISendToEmail _send;

        public HomeController(ISendToEmail send)
        {
            _send = send;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FromToVM model)
        {
            if (ModelState.IsValid)
            {
                if (_send.SendMessage(model).Result)
                {
                    return RedirectToAction(nameof(Privacy));
                }
                ModelState.AddModelError(string.Empty, "try again..");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
