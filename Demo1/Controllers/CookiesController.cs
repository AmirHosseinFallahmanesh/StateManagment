using Demo1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            var cookies = HttpContext.Request.Cookies.ToList();
            return View(cookies);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddCart()
        {

            Cart cart = new Cart
            {
                Name = "My Shopping Cart",
                Lines = new List<CartLine>
                {
                    new CartLine
                    {
                        ProductName = "Apple",
                        Price = 1,
                        Quantity = 3
                    },
                    new CartLine
                    {
                        ProductName = "Banana",
                        Price = 2,
                        Quantity = 5
                    },
                    new CartLine
                    {
                        ProductName = "Orange",
                        Price = 3,
                        Quantity = 2
                    },
                    new CartLine
                    {
                        ProductName = "Grapes",
                        Price = 4,
                        Quantity = 1
                    }
                }
            };

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.IsEssential = true;
            options.HttpOnly = true;
            options.SameSite = SameSiteMode.Strict;

            string value = JsonConvert.SerializeObject(cart);
            HttpContext.Response.Cookies.Append("cart", value, options);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string key)
        {
            if (Request.Cookies.ContainsKey(key))
            {
                Response.Cookies.Delete(key);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string key, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.IsEssential = true;
            options.HttpOnly = true;
            options.SameSite = SameSiteMode.Strict;

            HttpContext.Response.Cookies.Append(key, value,options);
            return RedirectToAction("Index");
        }

       
    }
}
