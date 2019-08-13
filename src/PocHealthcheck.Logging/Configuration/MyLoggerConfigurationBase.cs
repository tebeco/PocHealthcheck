using Microsoft.Extensions.Logging;

namespace PocHealthcheck.Logging.Configuration
{
    public abstract class MyLoggerConfigurationBase
    {
        public bool Enabled { get; set; } = true;

        public abstract LogLevel MinimumLevel { get; set; }
    }
}
