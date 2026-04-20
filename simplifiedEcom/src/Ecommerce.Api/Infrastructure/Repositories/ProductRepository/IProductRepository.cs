namespace Ecommerce.Api.Infrastructure.Repositories.ProductRepository;

using Domain.Product;

public interface IProductRepository
{
    Task<List<GetAllProductDto>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
}