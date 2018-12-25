using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace HttpClientFactoryLib.Monitoring
{
    public abstract class CachedHealthCheckBase : IHealthCheck
    {
        public const string LAST_UPDATE = nameof(LAST_UPDATE);
        protected HealthCheckResult cachedHealthCheckResult = HealthCheckResult.Unhealthy();

        public abstract Task<HealthCheckResult> ComputeAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken));

        public async Task UpdateAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await ComputeAsync(context, cancellationToken).ConfigureAwait(false);
            var data = new Dictionary<string, object>(result.Data.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            data[LAST_UPDATE] = DateTime.UtcNow;

            cachedHealthCheckResult = new HealthCheckResult(result.Status, result.Description, result.Exception, data);
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(cachedHealthCheckResult);
        }
    }
}
