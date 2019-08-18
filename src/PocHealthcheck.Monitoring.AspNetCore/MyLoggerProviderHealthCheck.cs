using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using PocHealthcheck.Logging.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MyLoggerProviderHealthCheck : IHealthCheck
    {
        private readonly MyLoggerFactoryOptions _options;

        public MyLoggerProviderHealthCheck(IOptions<MyLoggerFactoryOptions> options)
        {
            _options = options.Value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var loggerProviderRegistration = _options.Registrations.Single(loggerRegistration => loggerRegistration.Name == context.Registration.Name);
            var healthStatus = HealthStatus.Healthy;
            if (DateTime.UtcNow - loggerProviderRegistration.LastFailure < loggerProviderRegistration.ElasticsearchConfiguration.FailureDelay)
            {
                healthStatus = HealthStatus.Degraded;
            }

            return Task.FromResult(new HealthCheckResult(healthStatus, context.Registration.Name));
        }
    }
}