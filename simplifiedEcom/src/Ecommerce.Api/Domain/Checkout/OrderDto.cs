namespace Ecommerce.Api.Domain.Checkout;

public class OrderDto
{
    public int OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    
    public string ShippingName { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingPostalCode { get; set; }
    
    public List<ReturnedItemsDto> Items { get; set; }
    
}