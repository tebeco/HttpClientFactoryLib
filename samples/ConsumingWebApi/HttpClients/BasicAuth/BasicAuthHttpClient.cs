using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumingWebApi.HttpClients.BasicAuth
{

    public class BasicAuthHttpClient : IBasicAuthHttpClient
    {
        private readonly HttpClient httpClient;

        public BasicAuthHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                return await httpClient.GetStringAsync($"api/data/{Guid.NewGuid().ToString()}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}