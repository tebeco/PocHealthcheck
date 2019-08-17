using Microsoft.Extensions.Options;
using PocHealthcheck.Logging.Configuration;
using System.Linq;

namespace PocHealthcheck.Logging.Serilog
{
    public class MySerilogLoggerProviderFactory
    {
        private readonly IOptions<MyLoggerFactoryOptions> _options;

        public MySerilogLoggerProviderFactory(IOptions<MyLoggerFactoryOptions> options)
        {
            _options = options;
        }

        public MySerilogLoggerProvider CreateMySerilogProvider(string name)
        {
            var registration = _options.Value.Registrations.Single(registration => registration.Name == name);
            return new MySerilogLoggerProvider(registration);
        }
    }
}
