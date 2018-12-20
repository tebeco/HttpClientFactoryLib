using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegisterMultipleGenericType.HttpClients.Anonymous;

namespace RegisterMultipleGenericType.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpController : ControllerBase
    {
        private readonly IAnonymousHttpClient anonymousClient;

        public HttpController(IAnonymousHttpClient anonymousClient)
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
