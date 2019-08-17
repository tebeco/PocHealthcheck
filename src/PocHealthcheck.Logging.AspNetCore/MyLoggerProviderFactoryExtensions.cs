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
                .AddDefaultMySerilog()
                .AddInOutMySerilog()
                .AddHealthCheksMySerilog()
                ;
        }

        public static IMyLoggerProviderBuilder AddDefaultMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.AddMyLoggerProvider(MyLoggerConstants.DefaultLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                return factory.CreateProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddInOutMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.AddMyLoggerProvider(MyLoggerConstants.InOutLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<IMyLoggerProviderFactory>();
                return factory.CreateProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddHealthCheksMySerilog(this IMyLoggerProviderBuilder builder)
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