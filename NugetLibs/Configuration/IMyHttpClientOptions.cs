using System;

namespace NugetLibs.Configuration
{
    public interface IMyHttpClientOptions
    {
        Uri BaseAddress { get; set; }
        TimeSpan Timeout { get; set; }
        HealthCheckConfiguration HealthCheckConfiguration { get; set; }
    }
}