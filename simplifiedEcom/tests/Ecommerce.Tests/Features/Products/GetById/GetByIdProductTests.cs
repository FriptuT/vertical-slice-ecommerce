namespace Ecommerce.Tests.Features.Products.GetById;

using Api.Domain.Product;
using Api.Features.Products.GetAll;
using Api.Features.Products.GetById;
using Api.Infrastructure.Repositories.ProductRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetByIdProductTests
{
    private readonly Mock<IProductRepository> _repo;
    private readonly GetByIdProductHandler _handler;

    public GetByIdProductTests()
    {
        _repo = new Mock<IProductRepository>();
        _handler = new GetByIdProductHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_200Ok_With_One_Product()
    {
        // Arrange 
        var product = new Product
        {
            Id = 3,
            ImageUrl = "/images/Laptop.jpg",
            Name = "G Class",
            Price = 300000
        };
        _repo.Setup(repo => repo.GetByIdAsync(product.Id))
            .ReturnsAsync(product);
        // Act
        var result = await _handler.Handle(product.Id);

        // Assert
        result.Should().BeOfType<Ok<Product>>();
    }
}