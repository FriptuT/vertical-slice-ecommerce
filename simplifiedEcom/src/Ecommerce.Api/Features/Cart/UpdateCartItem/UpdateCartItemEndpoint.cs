namespace Ecommerce.Api.Features.Cart.UpdateCartItem;

using Domain.Cart;

public static class UpdateCartItemEndpoint
{
    public static void MapPutUpdateCartItemEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/cart/{productId:int}",
            async (
                int productId,
                UpdateCartItemRequest request,
                UpdateCartItemHandler handler
            ) =>
            {
                handler.Handle(request.UserId, productId, request.Quantity);
            });
    }
}