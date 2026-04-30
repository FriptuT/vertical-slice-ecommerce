namespace Ecommerce.Api.Features.Cart.UpdateCartItem;

using Domain.Cart;
using Microsoft.AspNetCore.Mvc;

public static class UpdateCartItemEndpoint
{
    public static void MapPutUpdateCartItemEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/cart/update",
            async (
                [FromBody] UpdateCartItemRequest request,
                [FromServices] UpdateCartItemHandler handler
            ) =>
            {
              await handler.Handle(request.UserId, request.ProductId, request.Quantity);
            });
    }
}