using System.Text.Json;
using System.Reflection;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Localization;
using WebVisit.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Text;

namespace WebVisit.Utilities
{
    public static class Utils
    {
        public static string Dump(Object? obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
            return string.Empty;
        }

        public static T? _Dump<T>(string obj)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(obj);
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
            return default;
        }

        public static T? Des<T>(string obj)
        {
            try
            {
                return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(obj, typeof(T));
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
            return default;
        }

        public static void WriteLog(string str) 
        {
            // MethodBase.GetCurrentMethod().Name;
            StackTrace stackTrace = new();
            MethodBase methodBase = stackTrace.GetFrame(1)?.GetMethod()!;
            Console.WriteLine(methodBase.Name+">"+str);
        }

        public static string Base64Encode(string plainText){
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData){
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string DecodeFromUtf8(this string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i=0;i<utf8String.Length;++i) {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }
            // return Encoding.Default.GetString(utf8Bytes, 0, utf8Bytes.Length);
            return Encoding.UTF8.GetString(utf8Bytes,0,utf8Bytes.Length);
        }

    }
}