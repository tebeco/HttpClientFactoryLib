using System.Threading.Tasks;

namespace ConsumingWebApi.HttpClients.Anonymous
{
    public interface IAnonymousHttpClient
    {
        Task<string> GetDataAsync();
    }
}