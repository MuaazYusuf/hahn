using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Domain.Base;
using Domain.Users.Interfaces;
using Application.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DDDConnectionString");
builder.Services.AddDbContextPool<EFContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IBaseAsyncRepository<,>), typeof(AsyncBaseRepository<,>))
                .AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
