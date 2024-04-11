using HttpClientFactoryLib.Http;

namespace ConsumingWebApi.HttpClients.Anonymous;

public class AnonymousHttpClientOptions : IMyHttpClientOptions
{
    public static string SectionName { get; } = "AnonymousHttpClient";

    public Uri BaseAddress { get; set; } = new Uri("https://localhost:5003/");

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);

    public HealthCheckConfiguration HealthCheckConfiguration { get; set; } = new HealthCheckConfiguration();

    public string HttpClientBuilderName { get; set; } = null!;
}