namespace Ecommerce.Tests.Features.Cart.RemoveCartItem;

using Api.Features.Cart.RemoveCartItem;
using Api.Infrastructure.Repositories.CartRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class RemoveCartItemHandlerTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly RemoveCartItemHandler _handler;

    public RemoveCartItemHandlerTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _handler = new RemoveCartItemHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_NotFound_When_Result_is_False()
    {
        // Arrange
        bool isDeleted = false;

        _repositoryMock.Setup(repo => repo.RemoveCartItem(1, 23))
            .ReturnsAsync(false);
        
        // Act
        var result = await _handler.Handle(1, 23);
        
        // Assert
        result.Should().BeOfType<NotFound>();
    }
    
    [Fact]
    public async Task Handler_Should_Return_NoContent_When_Result_is_True()
    {
        // Arrange
        bool isDeleted = true;

        _repositoryMock.Setup(repo => repo.RemoveCartItem(1, 23))
            .ReturnsAsync(true);
        
        // Act
        var result = await _handler.Handle(1, 23);
        
        // Assert
        result.Should().BeOfType<NoContent>();
    }
}