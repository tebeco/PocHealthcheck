using Microsoft.Extensions.Logging;

namespace PocHealthcheck.Logging.Configuration
{
    public class MyConsoleConfiguration : MyLoggerConfigurationBase
    {
        public override LogLevel MinimumLevel { get; set; } = LogLevel.Information;
    }
}
