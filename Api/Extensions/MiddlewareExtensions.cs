using Api.Middlewares;

namespace Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ExceptionsHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsHandlerMiddleware>();
        }
    }
}