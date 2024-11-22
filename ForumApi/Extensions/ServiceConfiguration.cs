using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Users;
using Contracts.Services.JWT;
using Contracts.Services.Seeding;
using DataAccess.UnitOfWork;
using ForumApi.Middleware;
using Models.Configuration;
using Services.Entity.ApplicationUsers;
using Services.JWT;
using Services.Seeding;

namespace ForumApi.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            AddUnitOfWork(services);

            AddSeedingService(services);

            AddEntityServices(services);
            AddJwtServices(services);

            AddCustomMiddleware(services);
            AddConfiguration(services, configurationManager);
        }

        private static void AddSeedingService(IServiceCollection services)
        {
            services.AddScoped<ISeedingService, SeedingService>();
        }

        private static void AddJwtServices(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }

        private static void AddEntityServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddCustomMiddleware(IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        private static void AddConfiguration(IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<JWTConfiguration>(configurationManager.GetSection("JWT"));
        }
    }
}
