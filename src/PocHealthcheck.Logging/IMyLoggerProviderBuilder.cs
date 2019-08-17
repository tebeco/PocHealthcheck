using Microsoft.Extensions.DependencyInjection;

namespace PocHealthcheck.Logging
{
    public interface IMyLoggerProviderBuilder
    {

        IMyLoggerProviderBuilder Add(MyLoggerProviderRegistration registration);

        IServiceCollection Services { get; }
    }
}
