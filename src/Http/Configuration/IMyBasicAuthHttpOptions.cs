using System;

namespace HttpClientFactoryLib.Http.Configuration
{
    public interface IMyBasicAuthHttpClientOptions : IMyHttpClientOptions
    {
        BasicAuthConfiguration BasicAuthConfiguration {get;set;}
    }
}