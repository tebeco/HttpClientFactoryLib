using System.Text;
using HttpClientFactoryLib.Http.Basic;

namespace HttpClientFactoryLib.Http.Handlers;

public class BasicAuthenticationHandler : DelegatingHandler
{
    private readonly string authorizationHeaderValue;

    public BasicAuthenticationHandler(BasicAuthenticationConfiguration basicAuthenticationConfiguration)
    {
        authorizationHeaderValue = $"Basic {EncodeCredentials(basicAuthenticationConfiguration.UserName, basicAuthenticationConfiguration.Password)}";
    }

    private static string EncodeCredentials(string userName, string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
    {
        httpRequestMessage.Headers.Add("Authorization", authorizationHeaderValue);
        return await base.SendAsync(httpRequestMessage, cancellationToken);
    }
}