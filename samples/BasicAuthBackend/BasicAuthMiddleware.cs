using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BasicAuthBackend
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

            var (user, password) = DecodeBasicAuth(authorizationHeader.First());
            if (user == "abc" && password == "def")
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

        private (string, string) DecodeBasicAuth(string basicAuthAuthorizationHeader)
        {
            var decodedHeader = Encoding.UTF8.GetString(Convert.FromBase64String(basicAuthAuthorizationHeader));
            var splittedAuthHeader = decodedHeader.Split(':');

            var userName = splittedAuthHeader.Length > 0 ? splittedAuthHeader[0] : null;
            var password = splittedAuthHeader.Length > 1 ? splittedAuthHeader[1] : null;
            return (userName, password);
        }
    }
}