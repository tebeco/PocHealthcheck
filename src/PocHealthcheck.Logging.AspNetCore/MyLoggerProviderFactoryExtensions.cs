using Microsoft.Extensions.Logging;
using PocHealthcheck.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyLoggerProviderFactoryExtensions
    {
        public static IMyLoggerProviderBuilder AddMyLoggerProviders(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, MyLoggerFactory>();
            return new MyLoggerProviderBuilder(services);
        }
    }
}