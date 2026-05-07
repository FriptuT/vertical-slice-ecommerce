namespace Ecommerce.Api.Features.Checkout.Process;

using Domain.Checkout;

public static class ProcessCheckoutEndpoint
{
    public static void MapPostCheckoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/checkout", async (CheckoutRequest request, ProcessCheckoutHandler handler) =>
        {
            var order = await handler.Handle(request);
            
            return Results.Ok(order);
        });
    }
}