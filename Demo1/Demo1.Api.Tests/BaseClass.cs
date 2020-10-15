using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Demo1.Resolver;


namespace Demo1.Api.Tests
{
    public class BaseClass : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client;
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Set up the TestServer and HttpClient used to perform requests
        /// </summary>
        public BaseClass()
        {
            var startupAssembly = typeof(Demo1.API.Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath("", startupAssembly);

            var builder = new WebHostBuilder()
                .UseStartup<Demo1.API.Startup>()
                .UseEnvironment(Environments.Development)
            .ConfigureAppConfiguration((context, config) =>
            {
                Configuration = config
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
            })
            .ConfigureServices(o =>
            {
                o.AddBusiness();
                o.AddContext(Configuration);
                o.AddServices();
                o.AddRepository();

            });



            _server = new TestServer(builder);

            Client = _server.CreateClient();

            //AutoMapper.IMapper fakeMapper = new Mapper(new
            //                MapperProvider().GetMapperConfig());

            // Client.BaseAddress = new Uri("http://localhost:64273/");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;

            var applicationBasePath = AppContext.BaseDirectory;

            var directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

                if (projectDirectoryInfo.Exists)
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

    }
}
