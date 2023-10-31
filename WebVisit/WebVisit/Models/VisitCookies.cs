using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public class VisitCookies
    {
        private const string Delimiter = "-";
        private IRequestCookieCollection requestCookies { get; set; } = null!;   // Read
        private IResponseCookies responseCookies { get; set; } = null!; // Write

        /**
        Read
        var cookies = new VisitCookies(Request.Cookies);

        Write
        var cookies = new VisitCookies(Response.Cookies);
        */
        public VisitCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        public VisitCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }
    }
}