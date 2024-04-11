using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HttpClientFactoryLib.Monitoring;

public abstract class CachedHealthCheckBase : IHealthCheck
{
    public const string LAST_UPDATE = nameof(LAST_UPDATE);
    protected HealthCheckResult cachedHealthCheckResult = HealthCheckResult.Unhealthy();

    public abstract Task<HealthCheckResult> ComputeAsync(HealthCheckContext context, CancellationToken cancellationToken = default);

    public async Task UpdateAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var result = await ComputeAsync(context, cancellationToken);
        var data = new Dictionary<string, object>(result.Data.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        data[LAST_UPDATE] = DateTime.UtcNow;

        cachedHealthCheckResult = new HealthCheckResult(result.Status, result.Description, result.Exception, data);
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(cachedHealthCheckResult);
    }
}
