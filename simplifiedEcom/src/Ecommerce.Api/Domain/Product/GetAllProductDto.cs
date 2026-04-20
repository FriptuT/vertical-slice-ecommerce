namespace Ecommerce.Api.Domain.Product;

public class GetAllProductDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }
}