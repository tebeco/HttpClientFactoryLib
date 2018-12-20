using System;
using NugetLibs.Configuration;

namespace RegisterMultipleGenericType.HttpClients.BasicAuth
{
    public class BasicAuthHttpClientOptions : IMyBasicAuthHttpClientOptions
    {
        public Uri BaseAddress { get; set; } = new Uri("https://localhost:5001/");
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);
        public HealthCheckConfiguration HealthCheckConfiguration { get; set; } = new HealthCheckConfiguration() { HealtcheckUri = new Uri("/SampleHealthEndPoint") };

        public BasicAuthConfiguration BasicAuthConfiguration { get; set; } = new BasicAuthConfiguration() { UserName = "abc", Password = "def" };
    }
}