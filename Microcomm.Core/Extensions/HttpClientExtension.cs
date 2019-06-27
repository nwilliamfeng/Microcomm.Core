using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm
{
    public static class HttpClientExtension
    {
        public static HttpClient AppendHeaders(this HttpClient client,IDictionary<string,string> headers)
        {
            if (headers != null)
                headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
            return client;
        }

        public static HttpClient ClearHeaders(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            return client;
        }


       
    }
}
