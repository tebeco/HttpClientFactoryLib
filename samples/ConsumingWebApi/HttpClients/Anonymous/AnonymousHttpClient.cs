namespace ConsumingWebApi.HttpClients.Anonymous;


public class AnonymousHttpClient : IAnonymousHttpClient
{
    private readonly HttpClient _httpClient;

    public AnonymousHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync()
    {
        try
        {
            return await _httpClient.GetStringAsync($"api/data/{Guid.NewGuid().ToString()}");
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}