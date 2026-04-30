namespace Ecommerce.Api.Features.Cart.UpdateCartItem;

using Infrastructure.Repositories.CartRepository;
using Microsoft.AspNetCore.Http.HttpResults;

public class UpdateCartItemHandler
{
    private readonly ICartRepository _repository;

    public UpdateCartItemHandler(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(int userId, int productId, int quantity)
    {
        var updatedItem = await _repository.UpdateCartItem(userId, productId, quantity);
        
        return Results.Ok(updatedItem);
    }
}