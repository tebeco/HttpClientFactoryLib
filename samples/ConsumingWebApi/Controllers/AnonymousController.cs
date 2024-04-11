using ConsumingWebApi.HttpClients.Anonymous;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnonymousController : ControllerBase
{
    private readonly IAnonymousHttpClient _anonymousClient;

    public AnonymousController(IAnonymousHttpClient anonymousClient)
    {
        _anonymousClient = anonymousClient;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get(int id)
    {
        return await _anonymousClient.GetDataAsync();
    }
}
