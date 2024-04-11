using HttpClientFactoryLib.Http.Basic;
using HttpClientFactoryLib.Http.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HttpClientFactoryLib.Http;

public static class HttpClientFactoryExtensions
{
    public static IHttpClientBuilder AddMyHttpClient<TInterface, TClient, TOptions>(this IServiceCollection services)
        where TInterface : class
        where TClient : class, TInterface
        where TOptions : class, IMyHttpClientOptions, new()
    {
        var httpClientBuilder = services.AddHttpClient<TInterface, TClient>((serviceProvider, httpClient) =>
        {
            var httpClientOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
            httpClient.BaseAddress = httpClientOptions.BaseAddress;
            httpClient.Timeout = httpClientOptions.Timeout;
        });

        services.AddOptions<TOptions>()
            .BindConfiguration(TOptions.SectionName)
            .PostConfigure(options =>
            {
                options.HttpClientBuilderName = httpClientBuilder.Name;
            })
            .Validate(options => options.HttpClientBuilderName == httpClientBuilder.Name);

        services.AddHealthChecks()
                .AddMyHttpHealthCheck<TOptions>();

        return httpClientBuilder;
    }

    public static IHttpClientBuilder AddMyBasicAuthenticationHttpClient<TInterface, TClient, TOptions>(this IServiceCollection services)
        where TInterface : class
        where TClient : class, TInterface
        where TOptions : class, IMyBasicAuthenticationHttpClientOptions, new()
    {
        var httpClientBuilder = services.AddMyHttpClient<TInterface, TClient, TOptions>();
        httpClientBuilder.AddHttpMessageHandler(serviceProvider =>
        {
            var httpClientOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
            return new BasicAuthenticationHandler(httpClientOptions.BasicAuthenticationConfiguration);
        });

        return httpClientBuilder;
    }

}