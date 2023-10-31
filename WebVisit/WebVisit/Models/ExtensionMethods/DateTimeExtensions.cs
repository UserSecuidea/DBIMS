using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Security.Cryptography;
using WebVisit.Utilities;

namespace WebVisit.Models
{
    public static class DateTimeExtensions
    {
        public static double GetUnixEpoch(this DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() - 
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixTime.TotalSeconds;
        }        
    }
}
        