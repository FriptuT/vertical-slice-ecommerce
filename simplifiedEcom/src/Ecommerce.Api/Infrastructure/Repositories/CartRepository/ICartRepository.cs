namespace Ecommerce.Api.Infrastructure.Repositories.CartRepository;

using Domain.Cart;

public interface ICartRepository
{
    // AddToCart
    Task<CartItemDto> AddToCart(int userId, int productId, int quantity);
    // GetCart
    Task<List<CartItemDto>> GetCart(int userId);
    // UpdateCartItem
    Task<CartItemDto> UpdateCartItem(int userId, int productId, int quantity);
    // RemoveCartItem
    Task<bool> RemoveCartItem(int userId, int productId);
}