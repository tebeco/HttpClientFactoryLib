using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumingWebApi.HttpClients.BasicAuth;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicAuthController : ControllerBase
    {
        private readonly IBasicAuthHttpClient basicAuthHttpClient;

        public BasicAuthController(IBasicAuthHttpClient basicAuthHttpClient)
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
