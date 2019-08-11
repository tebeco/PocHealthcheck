using System.Net.Http;

namespace SampleApp.HttpClients.Foo
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