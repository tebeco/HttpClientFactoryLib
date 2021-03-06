using HttpClientFactoryLib.Http.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientFactoryLib.Http.Handlers
{
    public class BasicAuthHandler : DelegatingHandler
    {
        private readonly string authorizationHeaderValue;

        public BasicAuthHandler(BasicAuthConfiguration basicAuthConfiguration)
        {
            authorizationHeaderValue = $"Basic {EncodeBasicAuth(basicAuthConfiguration.UserName, basicAuthConfiguration.Password)}";
        }

        private static string EncodeBasicAuth(string userName, string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            httpRequestMessage.Headers.Add("Authorization", authorizationHeaderValue);
            return await base.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
        }
    }
}