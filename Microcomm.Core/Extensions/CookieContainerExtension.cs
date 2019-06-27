using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm
{
     public static class CookieContainerExtension
    {

        public static CookieContainer Clear(this CookieContainer container,Uri uri)
        {
            container.GetCookies(uri).OfType<Cookie>()
                .ToList().ForEach(x => x.Expired = true);
            return container;
        }

        public static CookieContainer AddRange(this CookieContainer container,Uri url, IDictionary<string, string> cookies)
        {
            if (cookies != null)
                cookies.ToList().ForEach(k => container.Add(url, new Cookie(k.Key, k.Value)));
            return container;
        }
    }
}
