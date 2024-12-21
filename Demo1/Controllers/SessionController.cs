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
