using HttpClientFactoryLib.Http;
using HttpClientFactoryLib.Http.Basic;

namespace ConsumingWebApi.HttpClients.BasicAuthentication;

public class BasicAuthenticationHttpClientOptions : IMyBasicAuthenticationHttpClientOptions
{
    public static string SectionName { get; } = "BasicAuthenticationHttpClient";

    public Uri BaseAddress { get; set; } = new Uri("https://localhost:5005/");

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);

    public HealthCheckConfiguration HealthCheckConfiguration { get; set; } = new HealthCheckConfiguration();

    public BasicAuthenticationConfiguration BasicAuthenticationConfiguration { get; set; } = new BasicAuthenticationConfiguration() { UserName = "abc", Password = "def" };

    public string HttpClientBuilderName { get; set; } = null!;
}