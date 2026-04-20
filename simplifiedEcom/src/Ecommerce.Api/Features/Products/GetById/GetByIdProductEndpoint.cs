namespace Ecommerce.Api.Features.Products.GetById;

public static class GetByIdProductEndpoint
{
    public static void MapGetByIdProduct(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:int}", async (int id,GetByIdProductHandler handler) => await handler.Handle(id));
    }
}