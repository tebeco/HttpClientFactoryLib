using System;
using HttpClientFactoryLib.Http.Configuration;
using HttpClientFactoryLib.Monitoring.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddMyHttpHealthCheck<O>(this IHealthChecksBuilder healthChecksBuilder)
            where O : class, IMyHttpClientOptions, new()
        {
            healthChecksBuilder.Services.AddOptions<HealthCheckServiceOptions>()
                .Configure<IOptions<O>>((options, httpClientOptions) =>
                {
                    options.Registrations.Add(new HealthCheckRegistration(
                        name: httpClientOptions.Value.Name,
                        factory: s => ActivatorUtilities.GetServiceOrCreateInstance<HttpHealthCheck<O>>(s),
                        failureStatus: null,
                        tags: null));
                });
            return healthChecksBuilder;
        }
    }
}