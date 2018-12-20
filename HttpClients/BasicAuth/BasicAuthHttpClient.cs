using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegisterMultipleGenericType.HttpClients.BasicAuth
{

    public class BasicAuthHttpClient
    {
        private readonly HttpClient httpClient;

        public BasicAuthHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetData()
        {
            return await httpClient.GetStringAsync($"/data_{Guid.NewGuid().ToString()}").ConfigureAwait(false);
        }
    }
}