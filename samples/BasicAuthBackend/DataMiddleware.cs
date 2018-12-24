using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BasicAuthBackend
{
    public class DataMiddleware
    {
        private readonly RequestDelegate next;

        public DataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync($"Some data {httpContext.Request.Path.ToUriComponent()}").ConfigureAwait(false);
        }
    }
}