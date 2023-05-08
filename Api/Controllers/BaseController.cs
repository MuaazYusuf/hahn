using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected JsonResult response(dynamic payload, int statusCode, string? message = null)
        {
            if (message == null)
            {
                switch (statusCode)
                {
                    case StatusCodes.Status201Created:
                        message = "Resource has been added successfully";
                        break;
                    case StatusCodes.Status500InternalServerError:
                        message = "Something went wrong";
                        break;
                    case StatusCodes.Status401Unauthorized:
                        message = "Unauthorized";
                        break;
                    default:
                        message = null;
                        break;
                }
            }
            var response = new
            {
                data = payload,
                code = statusCode,
                message = message
            };
            return new JsonResult(response);
        }
    }
}