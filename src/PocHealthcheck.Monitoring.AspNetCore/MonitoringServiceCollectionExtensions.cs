using System;
using System.Threading;
using System.Threading.Tasks;
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
                                                    new LoggerHealthCheck(),
                                                    HealthStatus.Unhealthy,
                                                    Array.Empty<string>()));
                }
            });

            return healthChecksBuilder;
        }
    }

    public class LoggerHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}