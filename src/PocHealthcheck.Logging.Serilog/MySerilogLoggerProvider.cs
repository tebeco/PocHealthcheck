using PocHealthcheck.Logging.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;

namespace PocHealthcheck.Logging.Serilog
{
    public class MySerilogLoggerProvider : IMyLoggerProvider
    {
        private readonly MyLoggerRegistration _myLoggerRegistration;
        private readonly LoggerConfiguration _serilogLoggerConfiguration;
        private readonly SerilogLoggerProvider _serilogLoggerProvider;

        public MySerilogLoggerProvider(MyLoggerRegistration myLoggerRegistration)
        {
            _myLoggerRegistration = myLoggerRegistration;

            _serilogLoggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: myLoggerRegistration.ConsoleConfiguration.MinimumLevel.ToSerilogLevel())
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
                MinimumLogEventLevel = myElasticsearchConfiguration.MinimumLevel.ToSerilogLevel(),
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
