using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PocHealthcheck.Logging;
using PocHealthcheck.Logging.Configuration;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyLoggerProviderServiceCollectionExtensions
    {
        public static IMyLoggerProviderBuilder AddMyLoggerProviders(this IServiceCollection services)
        {
            services.AddOptions<MyLoggerFactoryOptions>();
            services.AddSingleton<ILoggerFactory, MyLoggerFactory>();
            return new MyLoggerProviderBuilder(services);
        }

        public static IMyLoggerProviderBuilder AddPredefinedMySerilog(this IMyLoggerProviderBuilder builder)
        {
            return builder
                .AddDefaultMyLoggerProvider()
                .AddInOutMyLoggerProvider()
                .AddHealthCheksMyLoggerProvider()
                ;
        }

        public static IMyLoggerProviderBuilder AddDefaultMyLoggerProvider(this IMyLoggerProviderBuilder builder) =>
            builder.AddNamedMyLoggerProvider(MyLoggerConstants.DefaultLoggerName);

        public static IMyLoggerProviderBuilder AddInOutMyLoggerProvider(this IMyLoggerProviderBuilder builder) =>
            builder.AddNamedMyLoggerProvider(MyLoggerConstants.InOutLoggerName);

        public static IMyLoggerProviderBuilder AddHealthCheksMyLoggerProvider(this IMyLoggerProviderBuilder builder) =>
            builder.AddNamedMyLoggerProvider(MyLoggerConstants.HealthChecksLoggerName);

        public static IMyLoggerProviderBuilder AddNamedMyLoggerProvider(this IMyLoggerProviderBuilder builder, string providerName)
        {
            builder.AddMyLoggerProvider(providerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                var options = serviceProvider.GetRequiredService<IOptions<MyLoggerFactoryOptions>>();
                var registration = options.Value.Registrations.Single(registration => registration.Name == name);
                return factory.CreateProvider(registration);
            });

            return builder;
        }

    }
}