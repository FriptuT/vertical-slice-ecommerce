namespace Ecommerce.Tests.Features.Cart.AddToCart;

using Api.Domain.Cart;
using Api.Features.Cart.AddToCart;
using Api.Infrastructure.Repositories.CartRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class AddToCartHandlerTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly AddToCartHandler _handler;

    public AddToCartHandlerTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _handler = new AddToCartHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_200OK_With_CartItem()
    {
        // Arrange
        var expectedItem = new AddCartItemResponse
        {
            Id = 1,
            ProductId = 1,
            ImageUrl = "img.png",
            Description = "Test product",
            Price = 100,
            Quantity = 2,
            Total = 200
        };

        _repositoryMock.Setup(repo => repo.AddToCart(1, 100, 2))
            .ReturnsAsync(expectedItem);
        
        // Act
        var result = await _handler.Handle(1, 100, 2);
        
        // Assert
        result.Should().BeOfType<Ok<AddCartItemResponse>>();
    }
}