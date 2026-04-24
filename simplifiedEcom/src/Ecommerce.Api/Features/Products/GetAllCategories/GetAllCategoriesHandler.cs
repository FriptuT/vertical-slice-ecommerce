namespace Ecommerce.Api.Features.Products.GetAllCategories;

using Infrastructure.Repositories.FilterRepository;
using Microsoft.AspNetCore.Mvc;

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

        if (categories == null || !categories.Any())
        {
            return Results.NotFound();
        }
        else
        {
            return Results.Ok(categories);
        }
        
    }
}