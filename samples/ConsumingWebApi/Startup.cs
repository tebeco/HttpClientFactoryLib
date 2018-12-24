using System.Linq;
using ConsumingWebApi.HttpClients.Anonymous;
using ConsumingWebApi.HttpClients.BasicAuth;
using HttpClientFactoryLib.Http.Configuration;
using HttpClientFactoryLib.Monitoring.Flacky;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ConsumingWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<FlackyHealthCheckOptions>();

            services.AddHealthChecks()
                    .AddFlackyHealthCheck();

            services.AddMyHttpClient<IAnonymousHttpClient, AnonymousHttpClient, AnonymousHttpClientOptions>();
            services.AddMyBasicAuthHttpClient<IBasicAuthHttpClient, BasicAuthHttpClient, BasicAuthHttpClientOptions>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var healthChecksOptions = new HealthCheckOptions();
            healthChecksOptions.ResponseWriter = async (httpContext, healthReport) =>
            {
                httpContext.Request.ContentType = "application/json";

                var healChecksResult = new
                {
                    status = healthReport.Status.ToString(),
                    results = healthReport.Entries.Select(entry =>
                    {
                        entry.Value.Data.TryGetValue(nameof(IMyHttpClientOptions), out var httpClientOptions);

                        return new
                        {
                            name = (httpClientOptions as IMyHttpClientOptions)?.Name ?? entry.Key,
                            value = entry.Value.Status.ToString(),
                            exception = entry.Value.Exception?.Message
                        };
                    })
                };
                var result = JsonConvert.SerializeObject(healChecksResult, Formatting.Indented);

                await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
            };

            app.UseHealthChecks("/health", healthChecksOptions);
            app.UseMvc();

        }
    }
}
