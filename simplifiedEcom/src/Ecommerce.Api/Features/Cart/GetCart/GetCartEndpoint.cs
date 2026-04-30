namespace Ecommerce.Api.Features.Cart.GetCart;

public static class GetCartEndpoint
{
    public static void MapGetCartEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cart/{userId:int}", async (int userId, GetCartHandler handler) =>
        {
            return await handler.Handle(userId);
        });
    }
}