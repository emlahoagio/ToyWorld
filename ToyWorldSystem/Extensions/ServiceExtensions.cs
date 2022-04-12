using Contracts;
using Contracts.Services;
using Entities.Models;
using Hangfire;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Services;

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
                //.AllowCredentials()
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

        //UserAccessor services
        public static void ConfigureUsserAccessor(this IServiceCollection services)
            => services.AddScoped<IUserAccessor, UserAccessor>();

        //Firebase services
        public static void ConfigureFirebaseServices(this IServiceCollection services)
            => services.AddScoped<IFirebaseSupport, Repository.Services.FirebaseServices>();

        //Hasing services
        public static void ConfigureHasingServices(this IServiceCollection services)
            => services.AddScoped<IHasingServices, HasingServices>();

        //Crawl Japan Figure services
        public static void ConfigureCrawlDataFromJapanFigure(this IServiceCollection services)
            => services.AddScoped<ICrawlDataJapanFigureServices, CrawlDataJapanFigureServices>();

        //Crawl MyKingdom
        public static void ConfigureMyKingdomCrawlers(this IServiceCollection services)
            => services.AddScoped<ICrawlDataMyKingdomService, CrawlDataMyKingdomService>();

        //Hangfire scheduling
        public static void ConfigureHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("sqlConnection")));
            services.AddHangfireServer();
        }
    }
}
