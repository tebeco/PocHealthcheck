using Microsoft.Extensions.DependencyInjection;
using System;

namespace PocHealthcheck.Logging
{
    public static class MyLoggerProviderBuilderAddProviderExtensions
    {
        public static IMyLoggerProviderBuilder AddMyLoggerProvider(this IMyLoggerProviderBuilder builder, string name, IMyLoggerProvider instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return builder.Add(new MyLoggerProviderRegistration(name, instance));
        }

        public static IMyLoggerProviderBuilder AddMyLoggerProvider(this IMyLoggerProviderBuilder builder, string name, Func<IServiceProvider, string, IMyLoggerProvider> factory)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return builder.Add(new MyLoggerProviderRegistration(name, s => factory(s, name)));
        }

        public static IMyLoggerProviderBuilder AddMyLoggerProvider<T>(this IMyLoggerProviderBuilder builder, string name) where T : class, IMyLoggerProvider
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.Add(new MyLoggerProviderRegistration(name, s => ActivatorUtilities.GetServiceOrCreateInstance<T>(s)));
        }
    }
}
