using Microsoft.AspNetCore.Mvc;
using Application.Services.Users;
namespace Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.EndPoint + ApiConstants.UsersRoute)]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger
            , UserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}