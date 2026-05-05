namespace Ecommerce.Api.Domain.Cart;

public class AddCartItemResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}