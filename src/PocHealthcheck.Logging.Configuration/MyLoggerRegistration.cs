namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public class MyLoggerRegistration
    {
        public string Name { get; set; }

        public MyConsoleConfiguration ConsoleConfiguration { get; set; } = new MyConsoleConfiguration();

        public MyElasticsearchConfiguration ElasticsearchConfiguration { get; set; } = new MyElasticsearchConfiguration();
    }
}
