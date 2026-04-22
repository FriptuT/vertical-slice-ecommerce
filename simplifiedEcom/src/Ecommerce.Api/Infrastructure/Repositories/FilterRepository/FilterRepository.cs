namespace Ecommerce.Api.Infrastructure.Repositories.FilterRepository;

using DefaultNamespace;
using Domain.Brand;
using Domain.Category;
using Microsoft.Data.SqlClient;

public class FilterRepository : IFilterRepository
{
    private readonly Db _databaseConnection;

    public FilterRepository(Db databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = new List<CategoryDto>();

        using var connection = _databaseConnection.CreateConnection();

        var command = new SqlCommand(
            @"SELECT Id, Name FROM Categories"
            , connection);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            categories.Add(new CategoryDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return categories;
    }

    public async Task<List<SubcategoryDto>> GetByCategoryIdAsync(int categoryId)
    {
        var subcategories = new List<SubcategoryDto>();

        using var connection = _databaseConnection.CreateConnection();

        var command = new SqlCommand(
            @"
            SELECT Id,Name 
            FROM Subcategories
            WHERE CategoryId = @categoryId
            ORDER BY Name
            ", connection);

        command.Parameters.AddWithValue("@categoryId", categoryId);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            subcategories.Add(new SubcategoryDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return subcategories;
    }

    public async Task<List<BrandDto>> GetAllBrandsAsync()
    {
        var brands = new List<BrandDto>();

        using var connection = _databaseConnection.CreateConnection();

        var command = new SqlCommand(
            @"
            SELECT 
                b.Id,
                b.Name,
                COUNT(p.Id) as ProductCount
            FROM Brands b
            LEFT JOIN Products p
                ON p.BrandId = b.Id
            GROUP BY b.Id, b.Name
            ORDER BY b.Name
            ",connection);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            brands.Add(new BrandDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ProductCount = reader.GetInt32(2)
            });
        }

        return brands;
    }
}