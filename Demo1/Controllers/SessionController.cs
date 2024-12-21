using Demo1.Extensoins;
using Demo1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve all session keys
            var sessionKeys = HttpContext.Session.Keys.ToList();
            Dictionary<string, string> model = new Dictionary<string, string>();
            foreach (var key in sessionKeys)
            {
             string value=   HttpContext.Session.GetString(key);
                model.Add(key, value);
            }
            return View(model);
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

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToAction("Index");
        }



        // POST: /Session/Add
        [HttpPost]
        public IActionResult Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                HttpContext.Session.SetString(key, value);
            }
            return RedirectToAction("Index");
        }

        // POST: /Session/Delete
        [HttpPost]
        public IActionResult Delete(string key)
        {
            HttpContext.Session.Remove(key);
            return RedirectToAction("Index");
        }

       
    }
}
