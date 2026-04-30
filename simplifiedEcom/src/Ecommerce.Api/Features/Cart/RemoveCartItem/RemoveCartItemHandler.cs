namespace Ecommerce.Api.Features.Cart.RemoveCartItem;

using Infrastructure.Repositories.CartRepository;
using Microsoft.AspNetCore.Http.HttpResults;

public class RemoveCartItemHandler
{
    private readonly ICartRepository _repository;

    public RemoveCartItemHandler(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(int userId, int productId)
    {
        var isDeleted = await _repository.RemoveCartItem(userId, productId);

        if (!isDeleted)
        {
            return Results.NotFound();
        }
        
        return Results.NoContent();
    }
}