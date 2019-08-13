using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public class MySerilogLoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, MySerilogLoggerProvider> providers = new Dictionary<string, MySerilogLoggerProvider>();

        public MySerilogLoggerFactory(IOptions<MySerilogLoggerFactoryOptions> options)
        {
            providers = options.Value
                                .Registrations
                                .Select(registration => new MySerilogLoggerProvider(registration))
                                .ToDictionary(x => x.Name, x => x);
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            MySerilogLoggerProvider serilogLoggerProvider;
            if (!providers.TryGetValue(categoryName, out serilogLoggerProvider))
            {
                if (!providers.TryGetValue("default", out serilogLoggerProvider))
                {
                    throw new Exception();
                }
            }

            return serilogLoggerProvider.CreateLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }

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

    public class MySerilogLoggerFactoryOptions
    {
        public ICollection<MyLoggerRegistration> Registrations { get; } = new List<MyLoggerRegistration>();
    }

    public class MyLoggerRegistration
    {
        public string Name { get; set; }

        public MyConsoleConfiguration ConsoleConfiguration { get; set; } = new MyConsoleConfiguration();

        public MyElasticsearchConfiguration ElasticsearchConfiguration { get; set; } = new MyElasticsearchConfiguration();
    }

    public abstract class MyLoggingSinkConfigurationBase
    {
        public bool Enabled { get; set; } = true;

        public abstract LogLevel MinimumLevel { get; set; }

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

    public class MyConsoleConfiguration : MyLoggingSinkConfigurationBase
    {
        public override LogLevel MinimumLevel { get; set; } = LogLevel.Information;
    }

    public class MyElasticsearchConfiguration : MyLoggingSinkConfigurationBase
    {
        public override LogLevel MinimumLevel { get; set; } = LogLevel.Information;

        public Uri[] Nodes { get; } = new Uri[] { new Uri("http://localhost:9200") };
    }
}
