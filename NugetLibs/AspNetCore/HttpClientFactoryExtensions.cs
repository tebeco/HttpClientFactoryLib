using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NugetLibs.Configuration;
using RegisterMultipleGenericType.NugetLibs.Handlers;

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