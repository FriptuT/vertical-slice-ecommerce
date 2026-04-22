namespace Ecommerce.Api.Infrastructure.Repositories.FilterRepository;

using Domain.Brand;
using Domain.Category;

public interface IFilterRepository
{
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<List<SubcategoryDto>> GetByCategoryIdAsync(int categoryId);
    Task<List<BrandDto>> GetAllBrandsAsync();
}