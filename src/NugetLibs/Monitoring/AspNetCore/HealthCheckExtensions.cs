using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using NugetLibs.Configuration;
using RegisterMultipleGenericType.NugetLibs.Handlers;
using RegisterMultipleGenericType.NugetLibs.Monitoring;

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