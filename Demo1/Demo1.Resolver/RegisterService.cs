using Demo1.Core.Service;
using Demo1.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo1.Resolver
{
    public static class RegisterService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDemoService, Demo1Service>();

        }
    }
}
