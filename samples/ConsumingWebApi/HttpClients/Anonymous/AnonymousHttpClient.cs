using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumingWebApi.HttpClients.Anonymous
{

    public class AnonymousHttpClient : IAnonymousHttpClient
    {
        private readonly HttpClient httpClient;

        public AnonymousHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                return await httpClient.GetStringAsync($"data_{Guid.NewGuid().ToString()}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}