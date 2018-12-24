using System.Threading.Tasks;
using ConsumingWebApi.HttpClients.Anonymous;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly IAnonymousHttpClient anonymousClient;

        public AnonymousController(IAnonymousHttpClient anonymousClient)
        {
            this.anonymousClient = anonymousClient;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(int id)
        {
            return await anonymousClient.GetDataAsync().ConfigureAwait(false);
        }
    }
}
