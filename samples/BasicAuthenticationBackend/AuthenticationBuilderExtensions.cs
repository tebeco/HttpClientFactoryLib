using Microsoft.AspNetCore.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme)
        => builder.AddBasic(authenticationScheme, null);

    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, Action<BasicAuthenticationOptions>? configureOptions)
        => builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(authenticationScheme, configureOptions);
}
