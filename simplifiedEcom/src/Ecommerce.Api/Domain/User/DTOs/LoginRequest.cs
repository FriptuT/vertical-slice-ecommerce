namespace Ecommerce.Api.Domain.User.DTOs;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}