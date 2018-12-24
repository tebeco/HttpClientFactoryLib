using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AnonymousBackend
{
    public class AnoymousMiddleware
    {
        private readonly RequestDelegate next;

        public AnoymousMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path.StartsWithSegments("/health"))
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync($"healthy").ConfigureAwait(false);

                return;
            }

            await httpContext.Response.WriteAsync($"Some data {httpContext.Request.Path.ToUriComponent()}").ConfigureAwait(false);
        }
    }
}