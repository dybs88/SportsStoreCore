using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportsStore.Infrastructure.Extensions
{
    public static class SessionExtensions
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static IEnumerable<object> GetSessionValues(this ISession session)
        {
            List<object> result = new List<object>();

            foreach (var key in session.Keys)
                result.Add(session.GetJson<object>(key));

            return result;
        }

        public static int GetInt(this ISession session, string key)
        {
            int outputInt = 0;
            if (int.TryParse(session.GetString(key), out outputInt))
                return outputInt;
            else
                return default(int);
        }
    }
}
