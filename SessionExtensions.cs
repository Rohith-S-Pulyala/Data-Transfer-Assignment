using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DataTransfer_WebApp_Pulyala.Extensions
{
    public static class SessionExtensions
    {
        //<T> Functions and ISession used for Session
        public static void SetObject<T>(this ISession session, string key, T value) 
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T> (this ISession session, string key) 
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
