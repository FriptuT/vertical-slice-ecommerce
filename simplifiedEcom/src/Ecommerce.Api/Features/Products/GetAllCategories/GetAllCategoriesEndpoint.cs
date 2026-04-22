namespace Ecommerce.Api.Features.Products.GetAllCategories;

public static class GetAllCategoriesEndpoint
{
    public static void MapGetAllCategories(this IEndpointRouteBuilder app)
    {
        app
            .MapGet("api/categories", async (GetAllCategoriesHandler handler) 
                => await handler.Handle());
    }
}