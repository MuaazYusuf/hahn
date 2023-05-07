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

namespace Test;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly UserController _controller;
    private readonly Mock<IUserService> _userServiceMock;
    public UsersControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UserController>>();
        _controller = new UserController(_loggerMock.Object, _userServiceMock.Object);

    }

    [Fact]
    public async Task GetById_ShouldReturn200AndUserResponse_WhenUserExists()
    {
        // Arrange
        var id = 1;
        var user = new User
        {
            Id = id,
            Username = "testuser",
            LastName = "testlastname",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            IsActive = true
        };

        var expectedResponse = new UserResponse
        {
            Id = id,
            Username = "testuser",
            LastName = "testlastname",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            IsActive = true
        };
        _userServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<UserResponse>(okResult.Value);
        Assert.Equal(expectedResponse.Id, actualResponse.Id);
        Assert.Equal(expectedResponse.Username, actualResponse.Username);
        Assert.Equal(expectedResponse.LastName, actualResponse.LastName);
        Assert.Equal(expectedResponse.DateOfBirth, actualResponse.DateOfBirth);
        Assert.Equal(expectedResponse.PhoneNumber, actualResponse.PhoneNumber);
        Assert.Equal(expectedResponse.IsActive, actualResponse.IsActive);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        int userId = 1;
        _userServiceMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
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
            BirthDate = new DateTime(1990, 1, 1)
        };

        var userEntity = new User()
        {
            Email = request.Email,
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.BirthDate
        };

        _userServiceMock.Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(userEntity);

        // Act
        var result = await _controller.Add(request) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal(typeof(UserResponse), result.Value.GetType());
    }
}
