using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Service
{

    public interface ICookieService
    {
        void Set<T>(string key, T value, int day);
        T Get<T>(string key);

        void Delete(string key);
    }
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void Delete(string key)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public T Get<T>(string key)
        {
            var cookieValue = httpContextAccessor.HttpContext.Request.Cookies[key];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return default; // Return default value for the type
            }

            // Deserialize the JSON back to the object
            return JsonConvert.DeserializeObject<T>(cookieValue);
        }

        public void Set<T>(string key, T value, int day )
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
                SameSite = SameSiteMode.Strict
            };
            if (day>0)
            {
                cookieOptions.Expires = DateTime.Now.AddDays(day);
            }
            var jsonValue = JsonConvert.SerializeObject(value);
            httpContextAccessor.HttpContext.Response.Cookies.Append(key, jsonValue, cookieOptions);
        }
    }
}
