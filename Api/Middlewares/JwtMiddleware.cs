namespace Api.Middlewares;

using Api.Jwt;
using Application.Services.Users;
using Microsoft.Extensions.Options;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var email = jwtUtils.ValidateJwtToken(token);
        if (email != null)
        {
            // attach account to context on successful jwt validation
            context.Items["User"] = await userService.GetByEmailAsync(email);
        }

        await _next(context);
    }
}