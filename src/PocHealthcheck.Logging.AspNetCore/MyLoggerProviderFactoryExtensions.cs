using Microsoft.Extensions.Logging;
using PocHealthcheck.Logging;
using PocHealthcheck.Logging.Configuration;

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

        public static IMyLoggerProviderBuilder AddDefaultMyLoggerProvider(this IMyLoggerProviderBuilder builder)
        {
            builder.AddMyLoggerProvider(MyLoggerConstants.DefaultLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                return factory.CreateProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddInOutMyLoggerProvider(this IMyLoggerProviderBuilder builder)
        {
            builder.AddMyLoggerProvider(MyLoggerConstants.InOutLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                return factory.CreateProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddHealthCheksMyLoggerProvider(this IMyLoggerProviderBuilder builder)
        {
            builder.AddMyLoggerProvider(MyLoggerConstants.HealthChecksLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                return factory.CreateProvider(name);
            });

            return builder;
        }
    }
}