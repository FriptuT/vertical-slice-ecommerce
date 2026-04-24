namespace Ecommerce.Api.Features.Authentication;

using Domain.User.DTOs;

public static class AuthenticationEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (RegisterRequest req, AuthenticationHandler handler)
            => await handler.Register(req));

        app.MapPost("/api/auth/login", async (LoginRequest req, AuthenticationHandler handler)
            => await handler.Login(req));
    }
}