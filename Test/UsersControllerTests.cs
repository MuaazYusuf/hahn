using System.Threading.Tasks;
using Api.Controllers;
using Application.DTOs.Users;
using Application.Services.Users;
using Application.Services;
using Domain.Users.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data.Repositories.Users;
using Domain.Users.Interfaces;
using Application.Validators;
using Xunit.Abstractions;
using Application.DTOs;
using Newtonsoft.Json;
using System.Net;
using Application.DBExceptions;
using Microsoft.Extensions.Localization;
using Api.Resources;

namespace Test;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly UserController _controller;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<UserValidator> _userValidatorMock;
    private readonly ITestOutputHelper _output;
    private readonly Mock<IStringLocalizer<SharedResource>> _SharedResource;
    public UsersControllerTests(ITestOutputHelper output)
    {
        _output = output;
        _userRepositoryMock = new Mock<IUserRepository>();
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UserController>>();
        _userValidatorMock = new Mock<UserValidator>();
        _SharedResource = new Mock<IStringLocalizer<SharedResource>>();
        _controller = new UserController(_loggerMock.Object, _userServiceMock.Object, _userValidatorMock.Object, _SharedResource.Object);
    }

    [Fact]
    public async Task GetById_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var id = 1;
        var user = new User
        {
            Id = id,
            Username = "testuser",
            FirstName = "Test",
            LastName = "testlastname",
            Email = "testuser@test.com",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            IsActive = true
        };

        var expectedResponse = new UserResponse
        {
            Id = id,
            Username = "testuser",
            LastName = "testlastname",
            DateOfBirth = new DateTime(2000, 1, 1).ToShortDateString(),
            PhoneNumber = "1234567890",
            IsActive = true
        };
        _userServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetById(id);
        var serializedJson = JsonConvert.SerializeObject(result.Value);
        BaseResponse<UserResponse> deserializedJson = JsonConvert.DeserializeObject<BaseResponse<UserResponse>>(serializedJson);

        // // // Assert
        Assert.IsType<JsonResult>(result);
        Assert.NotNull(deserializedJson);
        Assert.Equal(StatusCodes.Status200OK, deserializedJson.code);
        Assert.Equal(user.Id, deserializedJson.data.Id);
        Assert.Equal(user.Username, deserializedJson.data.Username);
        Assert.Equal(user.FirstName, deserializedJson.data.FirstName);
        Assert.Equal(user.LastName, deserializedJson.data.LastName);
        Assert.Equal(user.Email, deserializedJson.data.Email);
        Assert.Equal(user.DateOfBirth.ToShortDateString(), deserializedJson.data.DateOfBirth);
        Assert.Equal(user.PhoneNumber, deserializedJson.data.PhoneNumber);
        Assert.Equal(user.IsActive, deserializedJson.data.IsActive);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        var id = 1;
        _userServiceMock.Setup(s => s.GetByIdAsync(id)).ThrowsAsync(new EntityNotFoundException($"Entity with id {id} was not found"));
        // Act and Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _controller.GetById(1));
    }

    [Fact]
    public async Task AddUser_ShouldReturn201AndUserResponse_WhenUserIsValid()
    {
        var request = new AddUserRequest()
        {
            Email = "test@test.com",
            Username = "testuser",
            FirstName = "Test",
            LastName = "User",
            Password = "password",
            PhoneNumber = "1234567890",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var userEntity = new User()
        {
            Email = request.Email,
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth
        };

        _userServiceMock.Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(userEntity);

        // Act
        var result = await _controller.Add(request);
        var serializedJson = JsonConvert.SerializeObject(result.Value);
        BaseResponse<UserResponse> deserializedJson = JsonConvert.DeserializeObject<BaseResponse<UserResponse>>(serializedJson);

        // Assert
        Assert.IsType<JsonResult>(result);
        Assert.NotNull(deserializedJson);
        Assert.Equal(StatusCodes.Status201Created, deserializedJson.code);
        Assert.IsType<UserResponse>(deserializedJson.data);
    }
}
