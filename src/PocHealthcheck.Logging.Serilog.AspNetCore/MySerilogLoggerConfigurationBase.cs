using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public abstract class MySerilogLoggerConfigurationBase : MyLoggerConfigurationBase
    {
        
        public LogEventLevel SerilogMinimumLevel => MapToSerilogLevel(MinimumLevel);

        private LogEventLevel MapToSerilogLevel(LogLevel loglevel)
        {
            switch (loglevel)
            {
                case LogLevel.None:
                case LogLevel.Trace:
                    return LogEventLevel.Verbose;
                case LogLevel.Debug:
                    return LogEventLevel.Debug;
                case LogLevel.Warning:
                    return LogEventLevel.Warning;
                case LogLevel.Error:
                    return LogEventLevel.Error;
                case LogLevel.Critical:
                    return LogEventLevel.Fatal;
                case LogLevel.Information:
                default:
                    return LogEventLevel.Information;
            }
        }
    }
}
