using Demo1.Business;
using Demo1.Core.Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo1.Resolver
{
    public static class RegisterBusiness
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IDemoBusiness, DemoBusiness>();
        }
    }
}
