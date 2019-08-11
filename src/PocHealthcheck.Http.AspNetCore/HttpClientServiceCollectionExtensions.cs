using System;
using Microsoft.Extensions.Configuration;
using PocHealthcheck.Http.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddMyHttpClient<TClient, TImplementation>(this IServiceCollection services, string name)
            where TClient : class
            where TImplementation : class, TClient
        {
            services.AddOptions<MyHttpClientOptions>(name)
                    .Configure<IConfiguration>((options, configuration) => configuration.Bind($"{MyHttpClientOptions.SectionName}:{name}"))
                    ;

            return services.AddHttpClient<TClient, TImplementation>(name);
        }


        public static IHttpClientBuilder AddMyHttpClient<TClient, TImplementation>(this IServiceCollection services, string name, Action<MyHttpClientOptions> configure)
            where TClient : class
            where TImplementation : class, TClient
        {
            var httpClientBuilder = services.AddMyHttpClient<TClient, TImplementation>(name);
            services.Configure<MyHttpClientOptions>(name, configure);

            return httpClientBuilder;
        }
    }
}