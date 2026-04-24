namespace Ecommerce.Api.Features.Products.GetAllSubcategories;

using Infrastructure.Repositories.FilterRepository;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetAllSubcategoriesHandler
{
    private readonly IFilterRepository _repo;

    public GetAllSubcategoriesHandler(IFilterRepository repo)
    {
        _repo = repo;
    }

    public async Task<IResult> Handle(int categoryId)
    {
        var subcategories = await _repo.GetByCategoryIdAsync(categoryId);

        if (subcategories == null || !subcategories.Any())
        {
            return Results.NotFound();
        }
        
        return Results.Ok(subcategories);
    }
}