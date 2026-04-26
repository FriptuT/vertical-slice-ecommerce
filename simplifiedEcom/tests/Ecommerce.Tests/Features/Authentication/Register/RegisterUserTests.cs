namespace Ecommerce.Tests.Features.Authentication.Register;

using Api.Domain.User;
using Api.Domain.User.DTOs;
using Api.Features.Authentication;
using Api.Infrastructure.Repositories.UserRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

public class RegisterUserTests
{
    private readonly Mock<IUserRepository> _repo;
    private readonly JwtService _jwt;
    private readonly AuthenticationHandler _handler;

    public RegisterUserTests()
    {
        _repo = new Mock<IUserRepository>();
        
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Key", "THIS_IS_A_VERY_SECRET_KEY_12345"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"},
            {"Jwt:ExpireMinutes", "60"}
        };

        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        _jwt = new JwtService(config);
        _handler = new AuthenticationHandler(_repo.Object, _jwt);
    }

    [Fact]
    public async Task Register_Method_Should_Return_400_Bad_Request_When_Email_Already_Exists()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Name = "Teodor",
            Email = "teodor@gmail.com",
            Password = "123pass"
        };

        _repo.Setup(repo => repo.EmailExistsAsync(request.Email))
            .ReturnsAsync(true);
        
        // Act
        var result = await _handler.Register(request);

        // Assert

        result.Should().BeOfType<BadRequest<string>>();
    }

    [Fact]
    public async Task Register_Method_Should_Return_200OK_When_Email_Doesnt_Exists_and_User_is_Created()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Name = "Teodor",
            Email = "teodor@gmail.com",
            Password = "123pass"
        };

        _repo.Setup(repo => repo.EmailExistsAsync(request.Email))
            .ReturnsAsync(false);
        _repo.Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _handler.Register(request);

        // Assert
        result.Should().BeOfType<Ok<string>>();
    }
}