using System;
using NugetLibs.Configuration;

namespace RegisterMultipleGenericType.HttpClients.Anonymous
{
    public class AnonymousHttpClientOptions : IMyHttpClientOptions
    {
        public Uri BaseAddress { get; set; } = new Uri("https://localhost:5001/");
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);
        public HealthCheckConfiguration HealthCheckConfiguration { get; set; } = new HealthCheckConfiguration() { HealtcheckUri = new Uri("/SampleHealthEndPoint") };
    }
}