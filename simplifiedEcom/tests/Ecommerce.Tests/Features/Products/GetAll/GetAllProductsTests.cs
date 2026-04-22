namespace Ecommerce.Tests.Features.Products.GetAll;

using Api.Domain.Product;
using Api.Features.Products.GetAll;
using Api.Infrastructure.Repositories.ProductRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetAllProductsTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly GetAllProductsHandler _handler;

    public GetAllProductsTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _handler = new GetAllProductsHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_200Ok_With_Products()
    {
        // Arrange 
        var products = new List<GetAllProductDto>
        {
            new GetAllProductDto
            {
                Id = 1,
                ImageUrl = "images/laptop.jpg",
                Name = "MacBook Air",
                Price = 2000
            },
            new GetAllProductDto
            {
                Id = 2,
                ImageUrl = "images/phone.jpg",
                Name = "MacBook Air2",
                Price = 2500
            }
        };
        _repositoryMock
            .Setup(repo => repo.GetAllAsync(null,null,null))
            .ReturnsAsync(products);

        // Act
        var result = await _handler.Handle(null,null,null);

        // Assert
        result.Should().BeOfType<Ok<List<GetAllProductDto>>>();
    }
}