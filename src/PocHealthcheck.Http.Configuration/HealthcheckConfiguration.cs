using System;

namespace PocHealthcheck.Http.Configuration
{
    public class HealthcheckConfiguration
    {
        public Uri Endpoint { get; set; } = new Uri("/health");

        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(10);

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);
    }
}