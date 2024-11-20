using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Users;
using Contracts.Services.JWT;
using DataAccess.UnitOfWork;
using ForumApi.Middleware;
using Models.Configuration;
using Services.Entity.ApplicationUsers;
using Services.JWT;

namespace ForumApi.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();

            AddCustomMiddleware(services);
            AddConfiguration(services, configurationManager);
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
