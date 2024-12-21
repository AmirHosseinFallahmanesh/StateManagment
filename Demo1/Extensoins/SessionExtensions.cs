using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Extensoins
{
    public static class SessionExtensions
    {
        
        public static void SetJson(this ISession session,string key,object data)
        {
            string value = JsonConvert.SerializeObject(data);
            session.SetString(key, value);
        }


        public static T GetJson<T>(this ISession session,string key)
        {
            string data = session.GetString(key);
            return data == null ? default : JsonConvert.DeserializeObject<T>(data);
        }
    }
}
