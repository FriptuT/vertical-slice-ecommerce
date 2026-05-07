namespace Ecommerce.Tests.Features.Cart.GetCart;

using Api.Domain.Cart;
using Api.Features.Cart.GetCart;
using Api.Infrastructure.Repositories.CartRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetCartHandlerTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _handler = new GetCartHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_200OK_With_CartItems()
    {
        // Arrange
        var items = new List<CartItemDto>
        {
            new CartItemDto
            {
                ImageUrl = "image.jpg",
                Description = "Testing description",
                Price = 300,
                Quantity = 1,
                Total = 300
            },
            new CartItemDto
            {
                ImageUrl = "laptop.jpg",
                Description = "Testing description2",
                Price = 2000,
                Quantity = 1,
                Total = 2000
            }
        };
        _repositoryMock.Setup(repo => repo.GetCart(1))
            .ReturnsAsync(items);
        
        // Act
        var result = await _handler.Handle(1);

        // Assert
        result.Should().BeOfType<Ok<List<CartItemDto>>>();
    }
}