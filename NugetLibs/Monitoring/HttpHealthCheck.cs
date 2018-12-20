using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using NugetLibs.Configuration;

namespace RegisterMultipleGenericType.NugetLibs.Monitoring
{
    public class HttpHealthCheck<TMyHttpClientOptions> : IHealthCheck
        where TMyHttpClientOptions : class, IMyHttpClientOptions, new()
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly TMyHttpClientOptions httpClientOptions;

        public HttpHealthCheck(IHttpClientFactory httpClientFactory, IOptions<TMyHttpClientOptions> httpClientOptions)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClientOptions = httpClientOptions.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var httpClient = httpClientFactory.CreateClient(httpClientOptions.GetType().FullName);
            var data = new Dictionary<string, object> { { nameof(IMyHttpClientOptions), httpClientOptions } };
            
            try
            {
                var result = await httpClient.GetAsync(httpClientOptions.HealthCheckConfiguration.HealtcheckUri).ConfigureAwait(false);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return new HealthCheckResult(HealthStatus.Healthy, "Some desc", null, data);
                }
                else if (httpClientOptions.HealthCheckConfiguration.IsCritical)
                {
                    return new HealthCheckResult(HealthStatus.Unhealthy, "Some desc", null, data);
                }
                else
                {
                    return new HealthCheckResult(HealthStatus.Degraded, "Some desc", null, data);
                }
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "Some desc", ex, data);
            }
        }
    }
}