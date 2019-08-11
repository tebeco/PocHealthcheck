namespace Microsoft.Extensions.DependencyInjection
{
    public static class MonitoringServiceCollectionExtensions
    {
        public static IHealthChecksBuilder AddMyMonitoring(this IServiceCollection services)
        {
            return services.AddHealthChecks();
        }
    }
}
