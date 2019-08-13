using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using PocHealthcheck.Logging;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureMyLogStack(this IWebHostBuilder webHostBuilder, Action<ILoggingBuilder> configureLogging)
        {
            webHostBuilder.ConfigureLogging((webHostBuilderContext, loggingBuilder) =>
            {
                loggingBuilder.Services.AddSingleton<ILoggerFactory, MyLoggerFactory>();

                configureLogging(loggingBuilder);
            });

            return webHostBuilder;
        }
    }
}
