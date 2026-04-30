namespace Ecommerce.Api.Features.Cart.GetCart;

using Infrastructure.Repositories.CartRepository;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetCartHandler
{
    private readonly ICartRepository _repository;

    public GetCartHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IResult> Handle(int userId)
    {
        var cartItems = await _repository.GetCart(userId);

        if (cartItems == null || !cartItems.Any())
        {
            return Results.NotFound();
        }
        
        return Results.Ok(cartItems);
    }
}