using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HttpClientFactoryLib.Monitoring.Flacky;

public class FlackyHealthCheckOptions
{
    public double FailureRate { get; internal set; } = 0.5;
}

public class FlackyHealthCheck : IHealthCheck
{
    private readonly double failureRate;
    private readonly Random random;
    private readonly static Task<HealthCheckResult> healthyResult = Task.FromResult(HealthCheckResult.Healthy());
    private readonly static Task<HealthCheckResult> unhealthyResult = Task.FromResult(HealthCheckResult.Unhealthy());

    public FlackyHealthCheck(IOptions<FlackyHealthCheckOptions> options)
    {
        failureRate = options.Value.FailureRate;
        random = new Random();
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return (random.NextDouble() < failureRate) 
                ? healthyResult
                : unhealthyResult;
    }
}