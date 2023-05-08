using Api.Middlewares;

namespace Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationMiddleware>();
        }
    }
}