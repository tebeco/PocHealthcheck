using PocHealthcheck.Logging.Configuration;
using System;

namespace PocHealthcheck.Logging
{
    public class MyLoggerRegistration
    {
        public MyLoggerRegistration(string name, IMyLoggerProvider instance)
        {
            Name = name;
            Factory = (_) => instance;
        }

        public MyLoggerRegistration(string name, Func<IServiceProvider, IMyLoggerProvider> factory)
        {
            Name = name;
            Factory = factory;
        }

        public string Name { get; set; }

        public Func<IServiceProvider, IMyLoggerProvider> Factory { get; set; }

        public MyConsoleConfiguration ConsoleConfiguration { get; set; } = new MyConsoleConfiguration();

        public MyElasticsearchConfiguration ElasticsearchConfiguration { get; set; } = new MyElasticsearchConfiguration();
    }
}
