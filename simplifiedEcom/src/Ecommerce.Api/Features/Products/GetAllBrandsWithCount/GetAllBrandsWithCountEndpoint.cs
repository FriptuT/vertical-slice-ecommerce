namespace Ecommerce.Api.Features.Products.GetAllBrandsWithCount;

public static class GetAllBrandsWithCountEndpoint
{
    public static void MapGetAllBrandsWithCount(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/brands", async (GetAllBrandsWithCountHandler handler) => await handler.Handle());
    }
}