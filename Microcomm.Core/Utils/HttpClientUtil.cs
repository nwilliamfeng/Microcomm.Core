using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microcomm
{
    public class HttpClientUtil
    {
        private readonly Uri _baseUrl;

        public HttpClientUtil(string baseUrl) => this._baseUrl = new Uri(baseUrl);

        public event EventHandler<HttpProgressEventArgs> HttpSendProgress;


        public event EventHandler<HttpProgressEventArgs> HttpReceiveProgress;




        private ProgressMessageHandler BuildProgressHandle()
        {
            var result = new ProgressMessageHandler();
            result.HttpReceiveProgress += (s, e) => this.HttpReceiveProgress?.Invoke(this, e);
            result.HttpSendProgress += (s, e) => this.HttpSendProgress?.Invoke(this, e);
            return result;
        }

        public async Task<T> Get<T>(string path, IDictionary<string, string> headers = null, IDictionary<string, string> cookie = null)
        {

            var cookieContainer = new CookieContainer();
            using (var progressMessageHandler = BuildProgressHandle())
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = HttpClientFactory.Create(handler, progressMessageHandler))
            {
                client.BaseAddress = this._baseUrl;
                if (cookie != null)
                    cookie.ToList().ForEach(k => cookieContainer.Add(_baseUrl, new Cookie(k.Key, k.Value)));

                if (headers != null)
                    headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                var httpResponse = await client.GetAsync(path);
                httpResponse.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
        }

        public async Task<T> PostWithJson<T>(string path, object value, IDictionary<string, string> headers = null, IDictionary<string, string> cookie = null)
        {

            var cookieContainer = new CookieContainer();
            using (var progressMessageHandler = BuildProgressHandle())
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = HttpClientFactory.Create(handler, progressMessageHandler))
            {
                client.BaseAddress = this._baseUrl;
                if (cookie != null)
                    cookie.ToList().ForEach(k => cookieContainer.Add(_baseUrl, new Cookie(k.Key, k.Value)));
                var s = JsonConvert.SerializeObject(value);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(value));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                if (headers != null)
                    headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                var httpResponse = await client.PostAsync(path, content);
                httpResponse.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
        }


        public async Task<byte[]> Download(string path, IDictionary<string, string> headers = null, IDictionary<string, string> cookie = null)
        {

            var cookieContainer = new CookieContainer();
            using (var progressMessageHandler = BuildProgressHandle())
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = HttpClientFactory.Create(handler, progressMessageHandler))
            {
                client.BaseAddress = this._baseUrl;
                if (cookie != null)
                    cookie.ToList().ForEach(k => cookieContainer.Add(_baseUrl, new Cookie(k.Key, k.Value)));

                if (headers != null)
                    headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                var httpResponse = await client.GetAsync(path);
                httpResponse.EnsureSuccessStatusCode();//用来抛异常的
                return await httpResponse.Content.ReadAsByteArrayAsync();
            }
        }

        public async Task<string> Upload(string path, string[] filePaths, IDictionary<string, string> headers = null, IDictionary<string, string> cookie = null)
        {
            foreach (var filePath in filePaths)
                if (!System.IO.File.Exists(filePath))
                    throw new ArgumentException("不存在的文件：" + filePath);

            var contents = filePaths.Select(filePath => new Tuple<string, byte[]>(Path.GetFileName(filePath), System.IO.File.ReadAllBytes(filePath))).ToList();

            var cookieContainer = new CookieContainer();
            using (var progressMessageHandler = BuildProgressHandle())
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = HttpClientFactory.Create(handler, progressMessageHandler))
            {

                client.BaseAddress = _baseUrl;
                if (cookie != null)
                    cookie.ToList().ForEach(k => cookieContainer.Add(_baseUrl, new Cookie(k.Key, k.Value)));
                if (headers != null)
                    headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                using (var content = new MultipartFormDataContent())
                {
                    contents.ForEach(x =>
                    {
                        var httpContent = new StreamContent(new MemoryStream(x.Item2));
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Microcomm.MimeTypeHelper.GetMimeType(Path.GetExtension(x.Item1)));
                        content.Add(httpContent, "upload_" + Path.GetFileName(x.Item1), x.Item1);
                    });

                    using (var message = await client.PostAsync(path, content))
                    {
                        var input = await message.Content.ReadAsStringAsync();

                        return input;
                    }
                }
            }
        }

    }
}
