using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Extensions
{
    public static class ServiceExtensions
    {
        //Cors services
        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        //IIS services
        public static void ConfigureIISIntegration(this IServiceCollection services)
            => services.Configure<IISOptions>(option => { });

        //Logger services
        public static void ConfigureLoggerServices(this IServiceCollection services)
            => services.AddScoped<ILoggerManager, LoggerManager>();

        //SQL services
        public static void ConfigureSqlServices(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<DataContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        //Repository manager services
        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();

        //Firebase services
        public static void ConfigureFirebaseServices(this IServiceCollection services)
    => services.AddScoped<IFirebaseSupport, FirebaseServices>();
    }
}
