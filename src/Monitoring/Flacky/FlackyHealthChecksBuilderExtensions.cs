using HttpClientFactoryLib.Monitoring.Flacky;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FlackyHealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddFlackyHealthCheck(this IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder.Services.AddOptions<HealthCheckServiceOptions>()
                .Configure(options =>
                {
                    options.Registrations.Add(new HealthCheckRegistration(
                        name: "Random",
                        factory: s => ActivatorUtilities.GetServiceOrCreateInstance<FlackyHealthCheck>(s),
                        failureStatus: null,
                        tags: null));
                });
            return healthChecksBuilder;
        }
    }
}