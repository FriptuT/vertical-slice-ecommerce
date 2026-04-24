namespace Ecommerce.Api.Domain.User.DTOs;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}