using Microsoft.AspNetCore.Mvc;
using Application.Services.Users;
using Application.DTOs.Users;
using Domain.Users.Entities;
using static BCrypt.Net.BCrypt;

namespace Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.EndPoint + ApiConstants.UsersRoute)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger
            , IUserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(new UserResponse()
            {
                Id = user.Id,
                Username = user.Username,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            var entity = new User()
            {
                Email = request.Email,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = HashPassword(request.Password, 12),
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.BirthDate
            };

            var user = await _service.AddAsync(entity);
            return user == null ? UnprocessableEntity() : Created("", new UserResponse()
            {
                Id = user.Id,
                Username = user.Username,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            });
        }
    }
}