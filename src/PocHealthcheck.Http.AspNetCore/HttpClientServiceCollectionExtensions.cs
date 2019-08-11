using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddMyHttpClient<TClient, TImplementation>(this IServiceCollection services, string name)
            where TClient : class
            where TImplementation : class, TClient
        {
            return services.AddHttpClient<TClient, TImplementation>(name);
        }
    }
}
