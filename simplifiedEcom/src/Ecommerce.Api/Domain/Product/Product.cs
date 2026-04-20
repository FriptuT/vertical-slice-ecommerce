namespace Ecommerce.Api.Domain.Product;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
    public string SubcategoryName { get; set; }
    public string BrandName { get; set; }
    public bool Availability { get; set; }
    public string Condition { get; set; }
    public string ImageUrl { get; set; }
}