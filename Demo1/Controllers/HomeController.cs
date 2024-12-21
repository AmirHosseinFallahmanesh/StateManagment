using Demo1.Models;
using Demo1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookieService cookieService;

        public HomeController(ILogger<HomeController> logger, ICookieService cookieService)
        {
            _logger = logger;
            this.cookieService = cookieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CkService()
        {
            cookieService.Set<string>("fromservice", "test", 1);
            return RedirectToAction("index");
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
