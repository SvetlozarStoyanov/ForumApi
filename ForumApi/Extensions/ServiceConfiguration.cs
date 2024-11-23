using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.CommentReplies;
using Contracts.Services.Entity.Comments;
using Contracts.Services.Entity.Posts;
using Contracts.Services.Entity.Subforums;
using Contracts.Services.Entity.Users;
using Contracts.Services.JWT;
using Contracts.Services.Managers;
using Contracts.Services.Seeding;
using DataAccess.UnitOfWork;
using ForumApi.Middleware;
using Models.Configuration;
using Services.Entity.CommentReplies;
using Services.Entity.Comments;
using Services.Entity.Posts;
using Services.Entity.Subforums;
using Services.Entity.Users;
using Services.JWT;
using Services.Managers;
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
            AddManagers(services);
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
            services.AddScoped<ISubforumService, SubforumService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentReplyService, CommentReplyService>();
        }

        private static void AddManagers(IServiceCollection services)
        {
            services.AddScoped<ISubforumManager, SubforumManager>();
            services.AddScoped<IPostManager, PostManager>();
            services.AddScoped<ICommentManager, CommentManager>();
            services.AddScoped<ICommentReplyManager, CommentReplyManager>();
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
