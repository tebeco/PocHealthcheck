using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PocHealthcheck.Logging.Configuration;
using PocHealthcheck.Logging.Serilog;

namespace PocHealthcheck.Logging
{
    public static class MySerilogLoggerProviderExtensions
    {
        public static IMyLoggerProviderBuilder AddPredefinedMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.Services.TryAddSingleton<MySerilogLoggerProviderFactory>();

            return builder
                .AddDefaultMySerilog()
                .AddInOutMySerilog()
                .AddHealthCheksMySerilog()
                ;
        }

        public static IMyLoggerProviderBuilder AddDefaultMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.Services.TryAddSingleton<MySerilogLoggerProviderFactory>();

            builder.AddMyLoggerProvider(MyLoggerConstants.DefaultLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<MySerilogLoggerProviderFactory>();
                return factory.CreateMySerilogProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddInOutMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.Services.TryAddSingleton<MySerilogLoggerProviderFactory>();

            builder.AddMyLoggerProvider(MyLoggerConstants.InOutLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<MySerilogLoggerProviderFactory>();
                return factory.CreateMySerilogProvider(name);
            });

            return builder;
        }

        public static IMyLoggerProviderBuilder AddHealthCheksMySerilog(this IMyLoggerProviderBuilder builder)
        {
            builder.Services.TryAddSingleton<MySerilogLoggerProviderFactory>();

            builder.AddMyLoggerProvider(MyLoggerConstants.HealthChecksLoggerName, (serviceProvider, name) =>
            {
                var factory = serviceProvider.GetRequiredService<MySerilogLoggerProviderFactory>();
                return factory.CreateMySerilogProvider(name);
            });

            return builder;
        }
    }
}
