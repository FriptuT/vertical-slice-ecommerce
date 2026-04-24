namespace Ecommerce.Api.Infrastructure.Repositories.UserRepository;

using DefaultNamespace;
using Domain.User;
using Microsoft.Data.SqlClient;

public class UserRepository: IUserRepository
{
    private readonly Db _databaseConnection;

    public UserRepository(Db _databaseConnection)
    {
        _databaseConnection = _databaseConnection;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        using var connection = _databaseConnection.CreateConnection();

        var cmd = new SqlCommand(@"
        SELECT COUNT(1) FROM Users WHERE Email = @Email",connection);

        cmd.Parameters.AddWithValue("@Email", email);

        await connection.OpenAsync();

        return (int)await cmd.ExecuteScalarAsync() > 0;
    }

    public async Task CreateUserAsync(User user)
    {
        using var connection = _databaseConnection.CreateConnection();

        var cmd = new SqlCommand(@"INSERT INTO Users (Email, PasswordHash, Name) VALUES (@Email, @PasswordHash, @Name)", connection);

        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
        cmd.Parameters.AddWithValue("@Name", user.Name);

        await connection.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _databaseConnection.CreateConnection();

        var cmd = new SqlCommand(
            @"
            SELECT Id, Email, PasswordHash, Name FROM Users WHERE Email = @Email",connection);

        cmd.Parameters.AddWithValue("@Email", email);

        await connection.OpenAsync();

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User()
            {
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                Name = reader.GetString(3)
            };
        }

        return null;
    }
}