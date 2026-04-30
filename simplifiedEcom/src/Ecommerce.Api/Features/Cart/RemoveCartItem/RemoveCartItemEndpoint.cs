namespace Ecommerce.Api.Features.Cart.RemoveCartItem;

using Domain.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

public static class RemoveCartItemEndpoint
{
    public static void MapDeleteRemoveCartItemEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/cart/{userId:int}/{productId:int}", async (int userId, int productId, [FromServices] RemoveCartItemHandler handler) =>
        {
            return await handler.Handle(userId, productId);
        });
    }
}