using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public static class RemoteClientExtentions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            return content;
        }

        public static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream?.CanRead != true)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
                {
                    var js = new JsonSerializer();
                    js.Serialize(jtw, value);
                    jtw.Flush();
                }
            }
        }

        public static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return httpContent;
        }

        public static void AddHttpHeader(this HttpRequestMessage requestMessage, string headerName, string headerValue)
        {
            if (!requestMessage.Headers.TryAddWithoutValidation(headerName, headerValue))
            {
                Logger.Warn($"Unable to add the Header while executing the RestAPI: '{requestMessage.RequestUri}' with Key: '{headerName}' and Value: '{headerValue}'");
            }
        }

        public static void AddHttpHeader(this HttpRequestMessage requestMessage, Dictionary<string, string> customsHeader)
        {
            if (customsHeader != null)
            {
                foreach (var header in customsHeader)
                {
                    requestMessage.AddHttpHeader(header.Key, header.Value);
                }
            }
        }

        public static void AddHttpMethod(this HttpRequestMessage requestMessage, string url, HttpMethod value)
        {
            //  Guard.Against.Null(requestMessage, nameof(requestMessage));
            //  Guard.Against.NullOrWhiteSpace(url, nameof(url));

            requestMessage.RequestUri = new Uri(url);
            requestMessage.Method = value;
        }

        public static async Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpRequestMessage request, object requestParam, CancellationToken token)
        {
            var customsStr = $"Executing RestAPI Url:'{request.RequestUri}' and Body :'{request.Content}'";
            try
            {
                Logger.Info(customsStr);
                using (var httpContent = CreateHttpContent(requestParam))
                {
                    request.Content = httpContent;
                    //request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    return await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //var customsStr = $"Executing RestAPI Url:'{request.RequestUri}' and Body :'{request.Content}'";

                Logger.Error(ex, customsStr);
                throw new Exception($"Exception Message: { ex.Message?.ToString() ?? ex.InnerException.Message}  While {customsStr}");
            }
        }
    }
}