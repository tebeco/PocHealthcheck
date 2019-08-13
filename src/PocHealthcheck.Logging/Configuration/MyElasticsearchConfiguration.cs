using Microsoft.Extensions.Logging;
using System;

namespace PocHealthcheck.Logging.Configuration
{
    public class MyElasticsearchConfiguration : MyLoggerConfigurationBase
    {
        public override LogLevel MinimumLevel { get; set; } = LogLevel.Information;

        public Uri[] Nodes { get; } = new Uri[] { new Uri("http://localhost:9200") };
    }
}
