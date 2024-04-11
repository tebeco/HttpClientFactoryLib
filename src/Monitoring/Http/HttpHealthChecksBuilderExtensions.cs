using HttpClientFactoryLib.Http;
using HttpClientFactoryLib.Monitoring.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpHealthChecksBuilderExtensions
{
    public static IHealthChecksBuilder AddMyHttpHealthCheck<TMyHttpClientOptions>(this IHealthChecksBuilder healthChecksBuilder)
        where TMyHttpClientOptions : class, IMyHttpClientOptions, new()
    {
        healthChecksBuilder.Services.AddTransient<HttpHealthCheck<TMyHttpClientOptions>>();

        healthChecksBuilder.Services.AddOptions<HealthCheckServiceOptions>()
            .Configure<IOptions<TMyHttpClientOptions>>((options, httpClientOptions) =>
            {
                options.Registrations.Add(new HealthCheckRegistration(
                    name: httpClientOptions.Value.HttpClientBuilderName,
                    factory: s => s.GetRequiredService<HttpHealthCheck<TMyHttpClientOptions>>(),
                    failureStatus: null,
                    tags: null));
            });

        return healthChecksBuilder;
    }
}