namespace Ecommerce.Api.Domain.Cart;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string Imageurl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}