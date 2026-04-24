namespace Ecommerce.Api.Features.Authentication;

using Domain.User;
using Domain.User.DTOs;
using Infrastructure.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;

public class AuthenticationHandler
{
    private readonly IUserRepository _repo;
    private readonly JwtService _jwt;

    public AuthenticationHandler(IUserRepository repo, JwtService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<IResult> Register(RegisterRequest request)
    {
        if (await _repo.EmailExistsAsync(request.Email))
        {
            return Results.BadRequest("Email already exists");
        }

        var hasher = new PasswordHasher<object>();
        
        var user = new User()
        {
            Email = request.Email,
            Name = request.Name,
            PasswordHash = hasher.HashPassword(null, request.Password)
        };

        await _repo.CreateUserAsync(user);

        return Results.Ok("User created");
    }
    
    public async Task<IResult> Login(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return Results.BadRequest("Invalid credentials");
        }

        var hasher = new PasswordHasher<object>();

        var result = hasher.VerifyHashedPassword(
            null,
            user.PasswordHash,
            request.Password);

        if (result != PasswordVerificationResult.Success)
        {
            return Results.BadRequest("Invalid credentials");
        }

        var token = _jwt.GenerateToken(user);

        return Results.Ok(new AuthResponse
        {
            Token = token
        });
    }
}