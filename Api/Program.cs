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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DDDConnectionString");
builder.Services.AddDbContextPool<EFContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IBaseAsyncRepository<,>), typeof(AsyncBaseRepository<,>))
                .AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>))
                .AddScoped<UserService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddControllers();

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

app.UseCors("default");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

app.Run();
