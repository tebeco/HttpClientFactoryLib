using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsumingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => AnonymousBackend.Program.Main(args));
            Task.Run(() => BasicAuthBackend.Program.Main(args));
            Task.Run(() => OAuthBackend.Program.Main(args));

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
