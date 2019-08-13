using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using PocHealthcheck.Logging.Serilog.AspNetCore;

namespace Microsoft.AspNetCore.Hosting
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureSerilog(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureLogging((webHostBuilderContext, loggingBuilder) =>
            {
                loggingBuilder.Services.Configure<MySerilogLoggerFactoryOptions>(options =>
                {
                    options.Registrations.Add(new MyLoggerRegistration() { Name = "default" });
                });
                loggingBuilder.Services.AddSingleton<ILoggerFactory, MySerilogLoggerFactory>();
            });

            return webHostBuilder;
        }
    }
}
