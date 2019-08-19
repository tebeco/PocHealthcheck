using Microsoft.Extensions.Options;
using PocHealthcheck.Logging.Configuration;
using System.Linq;

namespace PocHealthcheck.Logging.Serilog
{
    public class MySerilogLoggerProviderFactory : IMyLoggerProviderFactory
    {
        public IMyLoggerProvider CreateProvider(MyLoggerProviderRegistration registration)
        {
            return new MySerilogLoggerProvider(registration);
        }
    }
}
