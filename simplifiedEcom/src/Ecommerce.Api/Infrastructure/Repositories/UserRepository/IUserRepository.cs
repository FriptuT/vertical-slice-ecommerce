namespace Ecommerce.Api.Infrastructure.Repositories.UserRepository;

using Domain.User;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task CreateUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}