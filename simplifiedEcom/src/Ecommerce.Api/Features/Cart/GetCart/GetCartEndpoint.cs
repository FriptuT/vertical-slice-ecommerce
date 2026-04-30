namespace Ecommerce.Api.Features.Cart.GetCart;

using Microsoft.AspNetCore.Mvc;

public static class GetCartEndpoint
{
    public static void MapGetCartEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cart/{userId:int}", async (int userId, [FromServices] GetCartHandler handler) =>
        {
            return await handler.Handle(userId);
        });
    }
}