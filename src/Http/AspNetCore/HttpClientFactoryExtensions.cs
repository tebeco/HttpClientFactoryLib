using HttpClientFactoryLib.Http.Configuration;
using HttpClientFactoryLib.Http.Handlers;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientFactoryExtensions
    {
        public static IHttpClientBuilder AddMyHttpClient<I, T, O>(this IServiceCollection services)
        where I : class
        where T : class, I
        where O : class, IMyHttpClientOptions, new()
        {
            var httpClientBuilder = services.AddHttpClient<I, T>();
            httpClientBuilder.ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var httpClientOptions = serviceProvider.GetService<IOptions<O>>().Value;
                httpClient.BaseAddress = httpClientOptions.BaseAddress;
                httpClient.Timeout = httpClientOptions.Timeout;
            });

            services.AddHealthChecks()
                    .AddMyHttpHealthCheck<O>();

            return httpClientBuilder;
        }

        public static IHttpClientBuilder AddMyBasicAuthHttpClient<I, T, O>(this IServiceCollection services)
        where I : class
        where T : class, I
        where O : class, IMyBasicAuthHttpClientOptions, new()
        {
            var httpClientBuilder = services.AddMyHttpClient<I, T, O>();
            httpClientBuilder.AddHttpMessageHandler(serviceProvider =>
            {
                var httpClientOptions = serviceProvider.GetService<IOptions<O>>().Value;
                return new BasicAuthHandler(httpClientOptions.BasicAuthConfiguration);
            });
            return httpClientBuilder;
        }

    }
}