using Microsoft.AspNetCore.Mvc;
using Application.Services.Users;
using Application.DTOs.Users;
using Domain.Users.Entities;
using static BCrypt.Net.BCrypt;
using Application.Validators;
using Application.DTOs;
using Microsoft.Extensions.Localization;
using Api.Resources;

namespace Api.Controllers
{
    [Route(ApiConstants.EndPoint + ApiConstants.UsersRoute)]
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        private readonly UserValidator _userValidator;
        private readonly IStringLocalizer<SharedResource> _SharedResource;
        public UserController
        (
            ILogger<UserController> logger,
            IUserService service,
            UserValidator userValidator,
            IStringLocalizer<SharedResource> SharedResource
        )
        {
            _service = service;
            _logger = logger;
            _userValidator = userValidator;
            this._SharedResource = SharedResource;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<JsonResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return this.response(new UserResponse()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            }, StatusCodes.Status200OK, _SharedResource["DONE"]);
        }

        [HttpPost]
        // [SwaggerRequestExample(typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<JsonResult> Add([FromBody] AddUserRequest request)
        {
            var entity = new User()
            {
                Email = request.Email,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = HashPassword(request.Password, 12),
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth
            };
            await _userValidator.ValidateAsync(entity);
            var user = await _service.AddAsync(entity);
            return this.response(new UserResponse()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            }, StatusCodes.Status201Created, _SharedResource["Resource added successfully"]);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<JsonResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var user = await _service.GetByIdAsync(id);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.DateOfBirth = request.DateOfBirth;
            user.UpdatedAt = DateTime.Now;
            var updatedUser = await _service.UpdateAsync(user);
            return this.response(new UserResponse()
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = user.Email,
                DateOfBirth = updatedUser.DateOfBirth,
                PhoneNumber = updatedUser.PhoneNumber,
                IsActive = updatedUser.IsActive,
                UpdatedAt = (DateTime?)updatedUser.UpdatedAt
            }, StatusCodes.Status200OK, _SharedResource["Resource updated successfully"]);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<JsonResult> Delete(int id)
        {
            var user = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(user);
            return this.response("", StatusCodes.Status200OK, _SharedResource["Resource deleted successfully"]);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<UserResponse>>))]
        public async Task<JsonResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            var mappedUsers = users.Select(u => new UserResponse()
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                IsActive = u.IsActive,
                UpdatedAt = (DateTime?)u.UpdatedAt
            });
            return this.response(mappedUsers, StatusCodes.Status200OK, _SharedResource["DONE"]);
        }
    }
}