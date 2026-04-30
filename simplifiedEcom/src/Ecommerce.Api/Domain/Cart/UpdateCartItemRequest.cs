namespace Ecommerce.Api.Domain.Cart;

public class UpdateCartItemRequest
{
    public int UserId { get; set; }
    public int Quantity { get; set; }
}