namespace Ecommerce.Api.Features.Cart.AddToCart;

using Domain.Cart;
using Microsoft.AspNetCore.Mvc;

public static class AddToCartEndpoint
{
    public static void MapPostAddToCartEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/add", async ([FromBody]AddToCartRequest request, [FromServices]AddToCartHandler handler)
            => await handler.Handle(request.UserId, request.ProductId, request.Quantity));
    }
}