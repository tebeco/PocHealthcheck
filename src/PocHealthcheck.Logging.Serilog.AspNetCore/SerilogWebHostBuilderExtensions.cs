using Microsoft.Extensions.DependencyInjection;
using PocHealthcheck.Logging;
using PocHealthcheck.Logging.Configuration;
using PocHealthcheck.Logging.Serilog;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureMySerilogLogStack(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureMyLogStack(loggingBuilder =>
            {
                loggingBuilder.Services.Configure<MyLoggerFactoryOptions>(options =>
                {
                    var serilogProviderFactory = new Func<IServiceProvider, MySerilogLoggerProviderFactory>(s => ActivatorUtilities.GetServiceOrCreateInstance<MySerilogLoggerProviderFactory>(s));

                    var registration = AddNamedProvider("default", serilogProviderFactory);

                    options.Registrations.Add(registration);
                });
            });

            return webHostBuilder;
        }

        public static MyLoggerRegistration AddNamedProvider(string name, Func<IServiceProvider, MySerilogLoggerProviderFactory> serilogProviderFactory) =>
            new MyLoggerRegistration(name, s => serilogProviderFactory(s).Create(name));
    }
}
