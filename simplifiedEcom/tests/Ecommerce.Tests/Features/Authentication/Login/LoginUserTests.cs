namespace Ecommerce.Tests.Features.Authentication.Login;

using Api.Domain.User;
using Api.Domain.User.DTOs;
using Api.Features.Authentication;
using Api.Infrastructure.Repositories.UserRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

public class LoginUserTests
{
    private readonly Mock<IUserRepository> _repo;
    private readonly JwtService _jwt;
    private readonly AuthenticationHandler _handler;

    public LoginUserTests()
    {
        _repo = new Mock<IUserRepository>();
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Key", "THIS_IS_A_VERY_SECRET_KEY_12345zzz"},
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
    public async Task Login_Method_Should_Return_401_Unauthorised_When_User_is_Null()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "teodor@test.com",
            Password = "123pass"
        };
        _repo.Setup(repo => repo.GetByEmailAsync(loginRequest.Email))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Login(loginRequest);

        // Assert
        result.Should().BeOfType<UnauthorizedHttpResult>();
    }

    [Fact]
    public async Task Login_Method_Should_Return_401_Unauthorised_When_Password_Is_Invalid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "teodor@gmail.com",
            Password = "wrongPassword"
        };

        var hasher = new PasswordHasher<object>();

        var correctPassword = "123pass";

        var user = new User
        {
            Id = 1,
            Email = request.Email,
            Name = "Teodor",
            PasswordHash = hasher.HashPassword(null, correctPassword)
        };

        _repo.Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Login(request);

        // Assert
        result.Should().BeOfType<UnauthorizedHttpResult>();
    }

    [Fact]
    public async Task Login_Method_Should_Return_200_And_Token_When_Password_Is_Valid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "teodor@gmail.com",
            Password = "123pass"
        };

        var hasher = new PasswordHasher<object>();

        var user = new User
        {
            Id = 1,
            Email = request.Email,
            Name = "Teodor",
            PasswordHash = hasher.HashPassword(null, request.Password)
        };

        _repo.Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Login(request);

        // Assert
        result.Should().BeOfType<Ok<AuthResponse>>();
    }
}