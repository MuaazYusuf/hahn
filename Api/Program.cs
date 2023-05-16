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
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { Language.EN, Language.AR};
    options.SetDefaultCulture(Language.EN)
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
var connectionString = builder.Configuration.GetConnectionString("DDDConnectionString");
builder.Services.AddDbContextPool<EFContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IBaseAsyncRepository<,>), typeof(AsyncBaseRepository<,>))
                .AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>))
                .AddScoped<IUserService, UserService>();
builder.Services.AddTransient<UserValidator>();
builder.Services.AddControllers().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
              });
builder.Services.AddCors(o => o.AddPolicy("default", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.ExceptionsHandlerMiddleware();

app.UseCors("default");

app.UseHttpsRedirection();



app.UseRouting();

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

app.Run();
