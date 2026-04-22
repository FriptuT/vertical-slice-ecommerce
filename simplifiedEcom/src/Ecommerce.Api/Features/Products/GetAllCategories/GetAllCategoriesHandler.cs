namespace Ecommerce.Api.Features.Products.GetAllCategories;

using Infrastructure.Repositories.FilterRepository;

public class GetAllCategoriesHandler
{
    private readonly IFilterRepository _repo;

    public GetAllCategoriesHandler(IFilterRepository repo)
    {
        _repo = repo;
    }

    public async Task<IResult> Handle()
    {
        var categories = await _repo.GetAllCategoriesAsync();

        return Results.Ok(categories);
    }
}