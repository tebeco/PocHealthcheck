using System.Net.Http;

namespace SampleAppProvider.HttpClients.Foo
{
    public class FooClient : IFooClient
    {
        private readonly HttpClient _httpClient;

        public FooClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    }
}