namespace Ecommerce.Api.Features.Cart.AddToCart;

using Infrastructure.Repositories.CartRepository;

public class AddToCartHandler
{
    private readonly ICartRepository _cartRepository;

    public AddToCartHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IResult> Handle(int userId, int productId, int quantity)
    {
        var cartItem = await _cartRepository.AddToCart(userId, productId, quantity);

        return Results.Ok(cartItem);
    }
}