using Application.DBExceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
namespace Api.Middlewares
{
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbExecutionException exception:
                        var DBResponse = new ErrorResponse
                        {
                            Message = "An unexpected error occurred while executing the database operation!",
                            Errors = exception.Message,
                            Code = HttpStatusCode.BadRequest
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        JsonConvert.SerializeObject(DBResponse);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(DBResponse));
                        break;
                    case EntityNotFoundException exception:
                        var EntityResponse = new ErrorResponse
                        {
                            Code = HttpStatusCode.NotFound,
                            Message = "Not Found",
                            Errors = exception.Message
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(EntityResponse));
                        break;
                    case ValidationException exception:
                        var errors = exception.Errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                                                                .ToDictionary(g => g.Key, g => g.ToArray());
                        var response = new ErrorResponse
                        {
                            Message = "Validation Failed",
                            Errors = errors
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
