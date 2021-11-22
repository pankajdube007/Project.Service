using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class RemoteClient : IDisposable
    {
        private async Task<RemoteStatus> SendAsync(HttpClient client, HttpRequestMessage request, object requestParam, CancellationToken token)
        {
            using (var response = await client.SendAsync(request, requestParam, token).ConfigureAwait(false))
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                var res = await RemoteClientExtentions.StreamToStringAsync(stream).ConfigureAwait(false);
                var status = (int)response.StatusCode;
                return new RemoteStatus { StatusCode = status, Response = res };
            }
        }

        public async Task<RemoteStatus> PostAsync(string url, object requestParam = null, Dictionary<string, string> customsHeader = null, CancellationToken token = default(CancellationToken))
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                  //  request.Version = HttpVersion.Version10;
                    request.AddHttpHeader(customsHeader);
                    request.AddHttpMethod(url, HttpMethod.Post);
                    return await SendAsync(client, request, requestParam, token).ConfigureAwait(false);
                }
            }
        }

        public async Task<RemoteStatus> GetAsync(string url, object requestParam = null, Dictionary<string, string> customsHeader = null, CancellationToken token = default(CancellationToken))
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                 //   request.Version = HttpVersion.Version10;
                    request.AddHttpHeader(customsHeader);
                    request.AddHttpMethod(url, HttpMethod.Get);
                    return await SendAsync(client, request, requestParam, token).ConfigureAwait(false);
                }
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RemoteClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }

    public class RemoteStatus
    {
        public int StatusCode { get; set; }
        public string Response { get; set; }
    }
}