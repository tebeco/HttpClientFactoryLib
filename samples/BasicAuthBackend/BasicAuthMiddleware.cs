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
            if (!user.Equals("abc", StringComparison.InvariantCulture) || !password.Equals("def", StringComparison.InvariantCulture))
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync($"Unauthorized").ConfigureAwait(false);

                return;
            }

            await next.Invoke(httpContext).ConfigureAwait(false);
        }

        private (string, string) DecodeBasicAuth(string basicAuthAuthorizationHeader)
        {
            if (!basicAuthAuthorizationHeader.StartsWith("BASIC ", StringComparison.InvariantCultureIgnoreCase))
            {
                return (null, null);
            }

            var authPair = basicAuthAuthorizationHeader.Substring("BASIC ".Length);
            var decodedHeader = Encoding.UTF8.GetString(Convert.FromBase64String(authPair));
            var splittedAuthHeader = decodedHeader.Split(':');

            var userName = splittedAuthHeader.Length > 0 ? splittedAuthHeader[0] : null;
            var password = splittedAuthHeader.Length > 1 ? splittedAuthHeader[1] : null;
            return (userName, password);
        }
    }
}