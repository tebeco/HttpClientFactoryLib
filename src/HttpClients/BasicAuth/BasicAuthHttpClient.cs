using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegisterMultipleGenericType.HttpClients.BasicAuth
{
    public interface IBasicAuthHttpClient
    {
        Task<string> GetDataAsync();
    }

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
                return await httpClient.GetStringAsync($"data_{Guid.NewGuid().ToString()}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}