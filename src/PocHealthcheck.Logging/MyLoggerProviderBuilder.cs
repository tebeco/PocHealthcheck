using Microsoft.Extensions.DependencyInjection;
using PocHealthcheck.Logging.Configuration;
using System;

namespace PocHealthcheck.Logging
{
    public class MyLoggerProviderBuilder: IMyLoggerProviderBuilder
    {
        public MyLoggerProviderBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public IMyLoggerProviderBuilder Add(MyLoggerProviderRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            Services.Configure<MyLoggerFactoryOptions>(options =>
            {
                options.Registrations.Add(registration);
            });

            return this;
        }
    }
}
