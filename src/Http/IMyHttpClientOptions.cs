namespace HttpClientFactoryLib.Http;

public interface IMyHttpClientOptions
{
    static abstract string SectionName { get; }

    Uri BaseAddress { get; set; }

    TimeSpan Timeout { get; set; }

    HealthCheckConfiguration HealthCheckConfiguration { get; set; }

    string HttpClientBuilderName { get; set; }
}
