using System.Threading.Tasks;

namespace ConsumingWebApi.HttpClients.BasicAuth
{
    public interface IBasicAuthHttpClient
    {
        Task<string> GetDataAsync();
    }
}