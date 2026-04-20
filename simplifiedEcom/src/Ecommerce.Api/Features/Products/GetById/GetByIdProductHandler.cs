namespace Ecommerce.Api.Features.Products.GetById;

using Domain.Product;
using Infrastructure.Repositories.ProductRepository;

public class GetByIdProductHandler
{
    private readonly IProductRepository _repo;

    public GetByIdProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IResult> Handle(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        return Results.Ok(product);
    }
}