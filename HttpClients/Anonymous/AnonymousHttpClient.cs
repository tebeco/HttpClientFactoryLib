using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegisterMultipleGenericType.HttpClients.Anonymous
{
    public class AnonymousHttpClient
    {
        private readonly HttpClient httpClient;

        public AnonymousHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetData()
        {
            return await httpClient.GetStringAsync($"/data_{Guid.NewGuid().ToString()}").ConfigureAwait(false);
        }

    }
}