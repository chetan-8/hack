using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using AutoMapper;
using Demo1.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Resolver
{
    public static class RegisterContext
    {
        public static void AddContext(this IServiceCollection services, 
            IConfiguration configuration)
        {

            services.AddDbContext<Demo1Context>(options => options.UseSqlServer(configuration.GetSection("ConnectionStrings")["DbConnection"]));
        }

    }
}
