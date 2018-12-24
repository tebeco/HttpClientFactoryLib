using System;

namespace HttpClientFactoryLib.Http.Configuration
{
    public class HealthCheckConfiguration
    {
        public Uri HealthCheckUri { get; set; } = new Uri("health", UriKind.Relative);
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(10);
        public bool IsCritical { get; set; } = true;
        public bool IsDisabled { get; set; } = false;
    }
}