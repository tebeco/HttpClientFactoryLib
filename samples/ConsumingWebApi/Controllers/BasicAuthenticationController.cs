using ConsumingWebApi.HttpClients.BasicAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasicAuthenticationController : ControllerBase
{
    private readonly IBasicAuthenticationHttpClient _basicAuthenticationHttpClient;

    public BasicAuthenticationController(IBasicAuthenticationHttpClient basicAuthenticationHttpClient)
    {
        _basicAuthenticationHttpClient = basicAuthenticationHttpClient;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get(int id)
    {
        return await _basicAuthenticationHttpClient.GetDataAsync(id);
    }
}
