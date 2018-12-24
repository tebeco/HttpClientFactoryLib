using System;
using HttpClientFactoryLib.Http.Configuration;

namespace ConsumingWebApi.HttpClients.Anonymous
{
    public class AnonymousHttpClientOptions : IMyHttpClientOptions
    {
        public string Name { get; set; } = "AnonymousHttpClient";
        public Uri BaseAddress { get; set; } = new Uri("https://localhost:5003/Anonymous/");
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);
        public HealthCheckConfiguration HealthCheckConfiguration { get; set; } = new HealthCheckConfiguration();
    }
}