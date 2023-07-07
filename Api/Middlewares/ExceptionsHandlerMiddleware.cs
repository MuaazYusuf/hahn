using Application.DBExceptions;
using FluentValidation;
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
                        // JsonConvert.SerializeObject(new { errors = new { $"{exception.PropertyName} {exception.errorMessage}" = new[] { exception.errorMessage } } });
                        var DBResponse = new ErrorResponse
                        {
                            message = "An unexpected error occurred while executing the database operation!",
                            errors = new Dictionary<string, string[]> { { exception.PropertyName, new[] { exception.ErrorMessage } } },
                            code = HttpStatusCode.BadRequest
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        JsonConvert.SerializeObject(DBResponse);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(DBResponse));
                        break;
                    case EntityNotFoundException exception:
                        var EntityResponse = new ErrorResponse
                        {
                            code = HttpStatusCode.NotFound,
                            message = "Not Found",
                            errors = exception.Message
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
                            code = HttpStatusCode.BadRequest,
                            message = "Validation Failed",
                            errors = errors
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                        break;
                    default:
                        var defaultResponse = new ErrorResponse
                        {
                            code = HttpStatusCode.BadRequest,
                            message = ex.Message,
                            errors = ""
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        JsonConvert.SerializeObject(ex);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(defaultResponse));
                        break;
                }
            }
        }
    }
}
