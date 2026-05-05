namespace Ecommerce.Api.Domain.Checkout;

public class CheckoutRequest
{
    public int UserId { get; set; }
    
    public string ShippingName { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingPostalCode { get; set; }
}