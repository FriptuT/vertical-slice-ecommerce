namespace Ecommerce.Api.Features.Products.GetAll;

using Infrastructure.Repositories.ProductRepository;

public class GetAllProductsHandler
{
    private readonly IProductRepository _repo;

    public GetAllProductsHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IResult> Handle()
    {
        var products = await _repo.GetAllAsync();

        return Results.Ok(products);
    }
}