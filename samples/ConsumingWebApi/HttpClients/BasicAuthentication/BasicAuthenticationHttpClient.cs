namespace ConsumingWebApi.HttpClients.BasicAuthentication;

public class BasicAuthenticationHttpClient : IBasicAuthenticationHttpClient
{
    private readonly HttpClient _httpClient;

    public BasicAuthenticationHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync(int id)
    {
        try
        {
            return await _httpClient.GetStringAsync($"api/data/{id}");
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}