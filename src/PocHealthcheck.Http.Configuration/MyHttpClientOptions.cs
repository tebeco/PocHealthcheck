using System;

namespace PocHealthcheck.Http.Configuration
{
    public class MyHttpClientOptions
    {
        public const string SectionName = "My:HttpClients";

        public Uri Endpoint { get; set; }

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(2);

        public HealthcheckConfiguration Healthcheck { get; set; } = new HealthcheckConfiguration();
    }
}