using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RealmSyncClinetLibrary.APIClient
{
    public class APIServerHttpClientHandler : DelegatingHandler
    {
        private readonly string AccessToken;

        public APIServerHttpClientHandler(string accessToken) : this(new HttpClientHandler(), accessToken)
        {
        }

        public APIServerHttpClientHandler(HttpClientHandler handler, string accessToken) : base(handler)
        {
            AccessToken = accessToken;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-AuthToken", AccessToken);
            request.Headers.Add("Accept", "application/json");

            return base.SendAsync(request, cancellationToken);
        }
    }
}
