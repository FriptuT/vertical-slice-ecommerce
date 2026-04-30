namespace Ecommerce.Api.Features.Cart.RemoveCartItem;

using Microsoft.AspNetCore.SignalR;

public static class RemoveCartItemEndpoint
{
    public static void MapDeleteRemoveCartItemEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/cart/{productId:int}", async (int productId, int userId, RemoveCartItemHandler handler) =>
        {
            return await handler.Handle(userId, productId);
        });
    }
}