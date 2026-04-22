namespace Ecommerce.Api.Infrastructure.Repositories.ProductRepository;

using Domain.Product;

public interface IProductRepository
{
    Task<List<GetAllProductDto>> GetAllAsync(int? categoryId, int? subcategoryId, int? brandId);
    Task<Product> GetByIdAsync(int id);
}