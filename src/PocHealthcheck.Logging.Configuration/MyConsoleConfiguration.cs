using Microsoft.Extensions.Logging;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public class MyConsoleConfiguration : MyLoggerConfigurationBase
    {
        public override LogLevel MinimumLevel { get; set; } = LogLevel.Information;
    }
}
