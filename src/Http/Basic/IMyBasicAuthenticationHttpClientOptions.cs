namespace HttpClientFactoryLib.Http.Basic;

public interface IMyBasicAuthenticationHttpClientOptions : IMyHttpClientOptions
{
    BasicAuthenticationConfiguration BasicAuthenticationConfiguration { get; set; }
}