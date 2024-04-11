namespace ConsumingWebApi.HttpClients.BasicAuthentication;

public interface IBasicAuthenticationHttpClient
{
    Task<string> GetDataAsync(int id);
}