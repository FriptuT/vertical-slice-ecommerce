namespace Ecommerce.Tests.Features.Cart.UpdateCartItem;

using Api.Domain.Cart;
using Api.Features.Cart.UpdateCartItem;
using Api.Infrastructure.Repositories.CartRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class UpdateCartItemHandlerTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly UpdateCartItemHandler _handler;

    public UpdateCartItemHandlerTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _handler = new UpdateCartItemHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_200OK_With_Updated_Item()
    {
        // Arrange
        var updatedItem = new CartItemDto
        {
            ImageUrl = "img.jpg",
            Description = "Hello this quantity was updated",
            Price = 100,
            Quantity = 5,
            Total = 500
        };
        _repositoryMock.Setup(repo => repo.UpdateCartItem(1, 23, 5))
            .ReturnsAsync(updatedItem);
        
        // Act
        var result = await _handler.Handle(1, 23, 5);

        // Assert
        result.Should().BeOfType<Ok<CartItemDto>>();
    }
}