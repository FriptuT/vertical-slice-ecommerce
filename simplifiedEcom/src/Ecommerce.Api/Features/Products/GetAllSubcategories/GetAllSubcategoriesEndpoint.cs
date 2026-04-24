namespace Ecommerce.Api.Features.Products.GetAllSubcategories;

public static class GetAllSubcategoriesEndpoint
{
    public static void MapGetAllSubcategories(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/subcategories",
            async (int categoryId, GetAllSubcategoriesHandler handler) => await handler.Handle(categoryId));
    }
}