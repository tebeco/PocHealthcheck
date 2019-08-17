using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PocHealthcheck.Logging.Configuration;
using System;
using System.Collections.Generic;

namespace PocHealthcheck.Logging
{
    public class MyLoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, IMyLoggerProvider> _providers = new Dictionary<string, IMyLoggerProvider>();
        private readonly IServiceScopeFactory _scopeFactory;

        public MyLoggerFactory(IServiceScopeFactory scopeFactory, IOptions<MyLoggerFactoryOptions> options)
        {
            _scopeFactory = scopeFactory;

            using var scope = _scopeFactory.CreateScope();
            
            foreach (var registration in options.Value.Registrations)
            {
                var myLoggerProvider = registration.Factory(scope.ServiceProvider);
                _providers[myLoggerProvider.Name] = myLoggerProvider;
            }
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (!_providers.TryGetValue(categoryName, out var loggerProvider))
            {
                if (!_providers.TryGetValue(MyLoggerConstants.DefaultLoggerName, out loggerProvider))
                {
                    throw new ArgumentException("could not find any logger to use");
                }
            }

            return loggerProvider.CreateLogger(categoryName);
        }

        public void Dispose()
        {
            foreach (var provider in _providers.Values)
            {
                provider.Dispose();
            }
        }
    }
}
