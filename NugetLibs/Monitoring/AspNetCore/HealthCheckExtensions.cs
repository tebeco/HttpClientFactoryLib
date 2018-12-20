using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NugetLibs.Configuration;
using RegisterMultipleGenericType.NugetLibs.Handlers;
using RegisterMultipleGenericType.NugetLibs.Monitoring;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddMyHttpHealtCheck<O>(this IHealthChecksBuilder healthChecksBuilder)
            where O : class, IMyHttpClientOptions, new()
        {
            healthChecksBuilder.AddCheck<HttpHealthCheck<O>>(Guid.NewGuid().ToString());
            return healthChecksBuilder;
        }
    }
}