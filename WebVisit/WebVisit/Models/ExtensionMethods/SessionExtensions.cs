using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;

namespace WebVisit.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            Console.WriteLine("[SessionExtensions] SetObject");
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            Console.WriteLine("[SessionExtensions] GetObject");
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)) ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}