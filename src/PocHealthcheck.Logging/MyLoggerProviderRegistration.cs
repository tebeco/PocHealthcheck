using PocHealthcheck.Logging.Configuration;
using System;

namespace PocHealthcheck.Logging
{
    public class MyLoggerProviderRegistration
    {
        public MyLoggerProviderRegistration(string name, IMyLoggerProvider instance)
            :this(name, (_) => instance)
        {
        }

        public MyLoggerProviderRegistration(string name, Func<IServiceProvider, IMyLoggerProvider> factory)
        {
            Name = name;
            Factory = factory;

            ElasticsearchConfiguration.IndexPrefix = GetIndexPrefix(name, ElasticsearchConfiguration.IndexPrefix);
        }

        private string GetIndexPrefix(string name, string indexPrefix)
        {
            return name switch
            {
                MyLoggerConstants.DefaultLoggerName      => "logs",
                MyLoggerConstants.InOutLoggerName        => "inout",
                MyLoggerConstants.HealthChecksLoggerName => "health",
                _                                        => indexPrefix,
            };
        }

        public string Name { get; set; }

        public Func<IServiceProvider, IMyLoggerProvider> Factory { get; }

        public MyConsoleConfiguration ConsoleConfiguration { get; set; } = new MyConsoleConfiguration();

        public MyElasticsearchConfiguration ElasticsearchConfiguration { get; set; } = new MyElasticsearchConfiguration();

        public DateTime LastFailure { get; private set; } = DateTime.MinValue;

        public void OnFailure()
        {
            LastFailure = DateTime.UtcNow;
        }
    }
}
