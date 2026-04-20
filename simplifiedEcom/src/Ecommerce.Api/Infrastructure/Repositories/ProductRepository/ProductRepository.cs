namespace Ecommerce.Api.Infrastructure.Repositories.ProductRepository;

using DefaultNamespace;
using Domain.Product;
using Microsoft.Data.SqlClient;

public class ProductRepository : IProductRepository
{
    private readonly Db _databaseConnection;

    public ProductRepository(Db databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<List<GetAllProductDto>> GetAllAsync()
    {
        var products = new List<GetAllProductDto>();

        using var connection = _databaseConnection.CreateConnection();

        var selectAllProductsCommand = new SqlCommand(
                @"
                SELECT 
                    Id, 
                    ImageUrl, 
                    Price, 
                    Name FROM Products", connection);

        await connection.OpenAsync();

        using SqlDataReader readProducts = await selectAllProductsCommand.ExecuteReaderAsync();

        while (await readProducts.ReadAsync())
        {
            products.Add(new GetAllProductDto
            {
                Id = readProducts.GetInt32(0),
                ImageUrl = readProducts.GetString(1),
                Price = readProducts.GetDecimal(2),
                Name = readProducts.GetString(3)
            });
        }

        return products;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        using var connection = _databaseConnection.CreateConnection();

        var selectProductByIdcommand = new SqlCommand(
            @"
            SELECT 
                p.Id, 
                p.Name,
                p.Description, 
                p.Price, 
                c.Name AS CategoryName, 
                s.Name AS SubcategoryName, 
                b.Name AS BrandName,
                p.Availability,
                p.Condition,
                p.ImageUrl
            from Products AS p
            JOIN Categories AS c ON p.CategoryId = c.Id
            JOIN Subcategories AS s ON p.SubcategoryId = s.Id
            JOIN Brands AS b ON p.BrandId = b.Id
            WHERE p.Id = @id
            "
            ,connection);
        selectProductByIdcommand.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        using var productReader = await selectProductByIdcommand.ExecuteReaderAsync();

        while (await productReader.ReadAsync())
        {
            return new Product
            {
                Id = productReader.GetInt32(0),
                Name = productReader.GetString(1),
                Description = productReader.GetString(2),
                Price = productReader.GetDecimal(3),
                CategoryName = productReader.GetString(4),
                SubcategoryName = productReader.GetString(5),
                BrandName = productReader.GetString(6),
                Availability = productReader.GetBoolean(7),
                Condition = productReader.GetString(8),
                ImageUrl = productReader.GetString(9)
            };
        }

        return null;
    }
}