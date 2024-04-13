using ConsumingWebApi.HttpClients.Anonymous;
using ConsumingWebApi.HttpClients.BasicAuthentication;
using HttpClientFactoryLib.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
        .AddHealthChecks()
        .AddFlackyHealthCheck();

builder.Services.AddMyHttpClient<IAnonymousHttpClient, AnonymousHttpClient, AnonymousHttpClientOptions>();
builder.Services.AddMyBasicAuthenticationHttpClient<IBasicAuthenticationHttpClient, BasicAuthenticationHttpClient, BasicAuthenticationHttpClientOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var healthCheckJsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = async (httpContext, healthReport) =>
    {
        httpContext.Request.ContentType = "application/json";

        //var healChecksResult = new
        //{
        //    status = healthReport.Status.ToString(),
        //    results = healthReport.Entries.Select(entry =>
        //    {
        //        return new
        //        {
        //            name = entry.Key,
        //            value = entry.Value.Status.ToString(),
        //            exception = entry.Value.Exception?.Message
        //        };
        //    })
        //};

        await httpContext.Response.WriteAsJsonAsync(healthReport, healthCheckJsonSerializerOptions);
    }
});

app.MapControllers();

app.Run();
