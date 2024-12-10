using Microsoft.IdentityModel.Tokens;

namespace ForumApi.Middleware
{
    public class ExpiredTokenMiddleware
    {
        private readonly RequestDelegate next;

        public ExpiredTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token has expired");
                }
            }
        }
    }
}
