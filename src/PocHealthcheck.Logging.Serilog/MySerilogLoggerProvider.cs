using PocHealthcheck.Logging.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;
using System;

namespace PocHealthcheck.Logging.Serilog
{
    public class MySerilogLoggerProvider : IMyLoggerProvider
    {
        private readonly MyLoggerProviderRegistration _myLoggerRegistration;
        private readonly LoggerConfiguration _serilogLoggerConfiguration;
        private readonly SerilogLoggerProvider _serilogLoggerProvider;

        public MySerilogLoggerProvider(MyLoggerProviderRegistration myLoggerRegistration)
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

        public DateTime LastFailure { get; private set; } = DateTime.MinValue;

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _serilogLoggerProvider.CreateLogger(categoryName);
        }

        private ElasticsearchSinkOptions GetElasticsearchOptions(MyElasticsearchConfiguration myElasticsearchConfiguration)
        {
            return new ElasticsearchSinkOptions(myElasticsearchConfiguration.Nodes)
            {
                IndexFormat = $"{myElasticsearchConfiguration.IndexPrefix.ToLower()}-{{0:yyyyMMdd}}",
                MinimumLogEventLevel = myElasticsearchConfiguration.MinimumLevel.ToSerilogLevel(),
                EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                FailureCallback = ex => { LastFailure = DateTime.UtcNow; }
            };
        }

        public void Dispose()
        {
            _serilogLoggerProvider.Dispose();
        }
    }
}
