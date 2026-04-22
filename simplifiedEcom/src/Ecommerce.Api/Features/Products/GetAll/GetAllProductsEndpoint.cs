namespace Ecommerce.Api.Features.Products.GetAll;

public static class GetAllProductsEndpoint
{
    public static void MapGetAllProducts(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async (
            int? categoryId, int? subcategoryId, int? brandId,
            GetAllProductsHandler handler) => await handler.Handle(categoryId, subcategoryId, brandId));
    }
}