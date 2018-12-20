using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NugetLibs.Configuration;
using RegisterMultipleGenericType.NugetLibs.Handlers;

namespace NugetLibs
{
    public static class HttpClientFactoryExtensions
    {
        public static IHttpClientBuilder AddMyHttpClient<I, T, O>(this IServiceCollection services)
        where I : class
        where T : class, I
        where O : class, IMyHttpClientOptions, new()
        {
            var httpClientBuilder = services.AddHttpClient<I, T>();
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
                // var basicAuthClientOptions = serviceProvider.GetService<IOptions<O>>
                // return new BasicAuthHandler(basicAuthClientOptions.basicAuthConfiguration);
                return null;
            });
            return httpClientBuilder;
        }

    }
}