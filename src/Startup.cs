using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NugetLibs.Configuration;
using RegisterMultipleGenericType.FakeBackends;
using RegisterMultipleGenericType.HttpClients.Anonymous;
using RegisterMultipleGenericType.HttpClients.BasicAuth;

namespace RegisterMultipleGenericType
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMyHttpClient<IAnonymousHttpClient, AnonymousHttpClient, AnonymousHttpClientOptions>();
            services.AddMyBasicAuthHttpClient<IBasicAuthHttpClient, BasicAuthHttpClient, BasicAuthHttpClientOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Map("/Anonymous", applicationBuilder => applicationBuilder.UseMiddleware<AnoymousMiddleware>());
            app.Map("/Basic", applicationBuilder => applicationBuilder.UseMiddleware<BasicAuthMiddleware>());
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
                            name = (httpClientOptions as IMyHttpClientOptions)?.Name ??entry.Key,
                            value =  entry.Value.Status.ToString(),
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
