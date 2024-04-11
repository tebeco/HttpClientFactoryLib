using System.Net;
using System.Net.Http;
using HttpClientFactoryLib.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HttpClientFactoryLib.Monitoring.Http;

public class HttpHealthCheck<TMyHttpClientOptions> : IHealthCheck
    where TMyHttpClientOptions : class, IMyHttpClientOptions, new()
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TMyHttpClientOptions _httpClientOptions;

    public HttpHealthCheck(IHttpClientFactory httpClientFactory, IOptions<TMyHttpClientOptions> httpClientOptions)
    {
        _httpClientFactory = httpClientFactory;
        _httpClientOptions = httpClientOptions.Value;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient(_httpClientOptions.HttpClientBuilderName);

        var healthUri = _httpClientOptions.HealthCheckConfiguration.HealthCheckUri;
        
        try
        {
            var result = await httpClient.GetAsync(healthUri);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new HealthCheckResult(HealthStatus.Healthy, "Some desc");
            }
            else if (_httpClientOptions.HealthCheckConfiguration.IsCritical)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "Some desc");
            }
            else
            {
                return new HealthCheckResult(HealthStatus.Degraded, "Some desc");
            }
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(HealthStatus.Unhealthy, "Some desc", ex);
        }
    }
}