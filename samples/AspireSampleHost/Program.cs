using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var anonymousService = builder.AddProject<AnonymousBackend>("anonymous-backend");
var basicAuthenticationBackend = builder.AddProject<BasicAuthenticationBackend>("basicauth-backend");
var oauthService = builder.AddProject<OAuthBackend>("oauth-backend");

var consumingService = builder.AddProject<ConsumingWebApi>("consuming-service")
    .WithReference(anonymousService)
    .WithReference(basicAuthenticationBackend)
    .WithReference(oauthService);

var app = builder.Build();

app.Run();
