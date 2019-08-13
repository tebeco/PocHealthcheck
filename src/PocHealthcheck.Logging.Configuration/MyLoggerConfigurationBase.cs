using Microsoft.Extensions.Logging;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public abstract class MyLoggerConfigurationBase
    {
        public bool Enabled { get; set; } = true;

        public abstract LogLevel MinimumLevel { get; set; }
    }
}
