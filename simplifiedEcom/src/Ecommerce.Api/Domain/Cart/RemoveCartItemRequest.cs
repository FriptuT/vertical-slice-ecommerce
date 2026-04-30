namespace Ecommerce.Api.Domain.Cart;

public class RemoveCartItemRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}