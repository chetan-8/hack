using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Demo1.Resolver;

namespace Demo1.API
{
    public class Program
    {
        public static IWebHostEnvironment HostingEnvironment { get; set; }

        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var retval = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        // delete all default configuration providers
                        config.Sources.Clear();
                        HostingEnvironment = hostingContext.HostingEnvironment;
                        Configuration = config
                        .SetBasePath(HostingEnvironment.ContentRootPath)
                        //.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        //.AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables().Build();
                    });
                    webBuilder.ConfigureServices(o =>
                    {
                        o.AddBusiness();
                        o.AddContext(Configuration);
                        o.AddServices();
                        o.AddRepository();

                    });
                });

         

            return retval;
        }
    }
}
