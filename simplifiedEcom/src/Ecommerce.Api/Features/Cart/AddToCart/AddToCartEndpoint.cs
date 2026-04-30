namespace Ecommerce.Api.Features.Cart.AddToCart;

using Domain.Cart;

public static class AddToCartEndpoint
{
    public static void MapPostAddToCartEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/add", async (AddToCartRequest request, AddToCartHandler handler)
            => await handler.Handle(request.UserId, request.ProductId, request.Quantity));
    }
}