using System;
using HttpClientFactoryLib.Http.Configuration;
using HttpClientFactoryLib.Monitoring.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpHealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddMyHttpHealthCheck<TMyHttpClientOptions>(this IHealthChecksBuilder healthChecksBuilder)
            where TMyHttpClientOptions : class, IMyHttpClientOptions, new()
        {
            healthChecksBuilder.Services.AddOptions<HealthCheckServiceOptions>()
                .Configure<IOptions<TMyHttpClientOptions>>((options, httpClientOptions) =>
                {
                    options.Registrations.Add(new HealthCheckRegistration(
                        name: httpClientOptions.Value.Name,
                        factory: s => ActivatorUtilities.GetServiceOrCreateInstance<HttpHealthCheck<TMyHttpClientOptions>>(s),
                        failureStatus: null,
                        tags: null));
                });
            return healthChecksBuilder;
        }
    }
}