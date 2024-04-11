using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? authorizationHeader = Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var (userName, password) = DecodeCredentials(authorizationHeader);

        if (string.IsNullOrEmpty(userName))
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (string.IsNullOrEmpty(password))
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!userName.Equals("abc", StringComparison.InvariantCulture) || !password.Equals("def", StringComparison.InvariantCulture))
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var user = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, userName)], Scheme.Name));
        var ticket = new AuthenticationTicket(user, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private (string?, string?) DecodeCredentials(string authorizationHeader)
    {
        if (!authorizationHeader.StartsWith("BASIC ", StringComparison.InvariantCultureIgnoreCase))
        {
            return (null, null);
        }

        var credentials = authorizationHeader.Substring("BASIC ".Length);
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
        var splittedCredentials = decodedCredentials.Split(':');

        var userName = splittedCredentials.Length > 0 ? splittedCredentials[0] : null;
        var password = splittedCredentials.Length > 1 ? splittedCredentials[1] : null;
        return (userName, password);
    }
}
