using System;

namespace HttpClientFactoryLib.Http.Configuration
{
    public interface IMyHttpClientOptions
    {
        string Name { get; set; }
        Uri BaseAddress { get; set; }
        TimeSpan Timeout { get; set; }
        HealthCheckConfiguration HealthCheckConfiguration { get; set; }
    }
}