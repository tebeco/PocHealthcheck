using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocHealthcheck.Logging.AspNetCore
{
    public class MyLoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, MySerilogLoggerProvider> providers = new Dictionary<string, MySerilogLoggerProvider>();

        public MyLoggerFactory(IOptions<MyLoggerFactoryOptions> options)
        {
            providers = options.Value
                                .Registrations
                                .Select(registration => new MySerilogLoggerProvider(registration))
                                .ToDictionary(x => x.Name, x => x);
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            MySerilogLoggerProvider serilogLoggerProvider;
            if (!providers.TryGetValue(categoryName, out serilogLoggerProvider))
            {
                if (!providers.TryGetValue("default", out serilogLoggerProvider))
                {
                    throw new Exception();
                }
            }

            return serilogLoggerProvider.CreateLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
