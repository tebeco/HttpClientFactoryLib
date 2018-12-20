using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegisterMultipleGenericType.HttpClients.Anonymous;
using RegisterMultipleGenericType.HttpClients.BasicAuth;

namespace RegisterMultipleGenericType.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicHttpController : ControllerBase
    {
        private readonly IBasicAuthHttpClient basicAuthHttpClient;

        public BasicHttpController(IBasicAuthHttpClient basicAuthHttpClient)
        {
            this.basicAuthHttpClient = basicAuthHttpClient;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(int id)
        {
            return await basicAuthHttpClient.GetDataAsync().ConfigureAwait(false);
        }
    }
}
