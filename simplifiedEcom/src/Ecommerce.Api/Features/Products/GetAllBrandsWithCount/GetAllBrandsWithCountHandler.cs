namespace Ecommerce.Api.Features.Products.GetAllBrandsWithCount;

using Infrastructure.Repositories.FilterRepository;

public class GetAllBrandsWithCountHandler
{
    private readonly IFilterRepository _repo;

    public GetAllBrandsWithCountHandler(IFilterRepository repo)
    {
        _repo = repo;
    }

    public async Task<IResult> Handle()
    {
        var brands = await _repo.GetAllBrandsAsync();
        
        return Results.Ok(brands);
    }
}