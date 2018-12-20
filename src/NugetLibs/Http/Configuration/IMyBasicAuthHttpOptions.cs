using System;

namespace NugetLibs.Configuration
{
    public interface IMyBasicAuthHttpClientOptions : IMyHttpClientOptions
    {
        BasicAuthConfiguration BasicAuthConfiguration {get;set;}
    }
}