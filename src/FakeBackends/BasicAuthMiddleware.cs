using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RegisterMultipleGenericType.NugetLibs.Handlers;

namespace RegisterMultipleGenericType.FakeBackends
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync($"Forbiden").ConfigureAwait(false);

                return;
            }

            if (authorizationHeader.First() == BasicAuthHandler.EncodeBasicAuth("abc", "def"))
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync($"Unauthorized").ConfigureAwait(false);

                return;
            }

            if (httpContext.Request.Path.StartsWithSegments("/health"))
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync($"healthy").ConfigureAwait(false);

                return;
            }

            await httpContext.Response.WriteAsync($"Some protected data {httpContext.Request.Path.ToUriComponent()}").ConfigureAwait(false);
        }
    }
}