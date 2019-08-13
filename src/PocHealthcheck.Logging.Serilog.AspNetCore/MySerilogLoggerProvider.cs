using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public class MySerilogLoggerProvider : ILoggerProvider
    {
        private readonly MyLoggerRegistration _myLoggerRegistration;
        private readonly LoggerConfiguration _serilogLoggerConfiguration;
        private readonly SerilogLoggerProvider _serilogLoggerProvider;

        public MySerilogLoggerProvider(MyLoggerRegistration myLoggerRegistration)
        {
            _myLoggerRegistration = myLoggerRegistration;

            _serilogLoggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: myLoggerRegistration.ConsoleConfiguration.SerilogMinimumLevel)
                .WriteTo.Elasticsearch(GetElasticsearchOptions(myLoggerRegistration.ElasticsearchConfiguration))
                ;
            _serilogLoggerProvider = new SerilogLoggerProvider(_serilogLoggerConfiguration.CreateLogger(), true);
        }

        public string Name => _myLoggerRegistration.Name;

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _serilogLoggerProvider.CreateLogger(categoryName);
        }

        private ElasticsearchSinkOptions GetElasticsearchOptions(MyElasticsearchConfiguration myElasticsearchConfiguration)
        {
            return new ElasticsearchSinkOptions(myElasticsearchConfiguration.Nodes)
            {
                MinimumLogEventLevel = myElasticsearchConfiguration.SerilogMinimumLevel,
                EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                FailureCallback = ex => { }
            };
        }

        public void Dispose()
        {
            _serilogLoggerProvider.Dispose();
        }
    }
}
