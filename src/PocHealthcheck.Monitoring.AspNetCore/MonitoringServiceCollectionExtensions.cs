using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using PocHealthcheck.Logging.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MonitoringServiceCollectionExtensions
    {
        public static IHealthChecksBuilder AddMyMonitoring(this IServiceCollection services)
        {
            var healthChecksBuilder = services.AddHealthChecks();

            services.AddOptions<HealthCheckServiceOptions>()
                    .Configure<IOptions<MyLoggerFactoryOptions>>((options, loggerOptions) =>
            {
                foreach (var loggerRegistration in loggerOptions.Value.Registrations)
                {
                    options.Registrations.Add(
                        new HealthCheckRegistration(loggerRegistration.Name, 
                                                    sp => ActivatorUtilities.CreateInstance<MyLoggerProviderHealthCheck>(sp),
                                                    HealthStatus.Unhealthy,
                                                    Array.Empty<string>()));
                }
            });

            return healthChecksBuilder;
        }
    }
}