using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PocHealthcheck.Logging;
using PocHealthcheck.Logging.Serilog;

namespace SampleAppProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureServices(services =>
                    {
                        services.TryAddSingleton<IMyLoggerProviderFactory, MySerilogLoggerProviderFactory>();

                        services.AddMyLoggerProviders()
                                .AddPredefinedMySerilog()
                                ;
                    })
                    .UseStartup<Startup>();
    }
}
