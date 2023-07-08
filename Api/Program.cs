using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Domain.Base;
using Domain.Users.Interfaces;
using Application.Services.Users;
using Microsoft.OpenApi.Models;
using Application.Base;
using Application.Services;
using Api.Extensions;
using Application.Validators;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc.Razor;

using Api.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { Language.EN, Language.AR };
    options.SetDefaultCulture(Language.EN)
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
var connectionString = builder.Configuration.GetConnectionString("DDDConnectionString");
builder.Services.AddDbContextPool<EFContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped(typeof(IBaseAsyncRepository<,>), typeof(AsyncBaseRepository<,>))
                .AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>))
                .AddScoped<IUserService, UserService>();
builder.Services.AddTransient<UserValidator>();

builder.Services.AddCors(o => o.AddPolicy(ApiConstants.DEFAULT_CORS_POLICY, builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

builder.Services.AddControllers().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization().AddJsonOptions(x =>
    {   // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
              {
                  options.SwaggerDoc("v1", new OpenApiInfo()
                  {
                      Title = "Hahn APIs",
                      Version = "V1",
                      Description = "Api",
                  });
                  options.OperationFilter<ContentLanguageHeader>();
                  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      In = ParameterLocation.Header,
                      Description = "Please enter token",
                      Name = "Authorization",
                      Type = SecuritySchemeType.Http,
                      BearerFormat = "JWT",
                      Scheme = "bearer"
                  });
                  options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
              });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.ExceptionsHandlerMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(ApiConstants.DEFAULT_CORS_POLICY);
app.UseAuthorization();
app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
var supportedCultures = new[] { Language.EN, Language.AR };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    ApplyCurrentCultureToResponseHeaders = true
});
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EFContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
app.Run();
