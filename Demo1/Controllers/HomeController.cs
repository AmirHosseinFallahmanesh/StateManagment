using Demo1.Extensoins;
using Demo1.Models;
using Demo1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger, ICookieService cookieService,IMemoryCache memoryCache)
        {
            _logger = logger;
            this.cookieService = cookieService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            memoryCache.Set("cart", new Cart());
            memoryCache.Get<Cart>("cart");
            return View();
        }

        public IActionResult CacheDemo()
        {
            
            return View();
        }

        public IActionResult GetSessionCart()
        {

            Cart cart = HttpContext.Session.GetJson<Cart>("cart")??new Cart();
            return View(cart);
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
