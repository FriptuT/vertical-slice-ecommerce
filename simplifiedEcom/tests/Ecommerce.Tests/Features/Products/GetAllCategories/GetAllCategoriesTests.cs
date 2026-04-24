namespace Ecommerce.Tests.Features.Products.GetAllCategories;

using Api.Domain.Category;
using Api.Features.Products.GetAllCategories;
using Api.Infrastructure.Repositories.FilterRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetAllCategoriesTests
{
    private readonly Mock<IFilterRepository> _repositoryMock;
    private readonly GetAllCategoriesHandler _handler;

    public GetAllCategoriesTests()
    {
        _repositoryMock = new Mock<IFilterRepository>();
        _handler = new GetAllCategoriesHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_2O0_Ok_With_Categories()
    {
        // Arrange 
        var categories = new List<CategoryDto>
        {
            new CategoryDto
            {
              Id  = 1,
              Name = "Women"
            },
            new CategoryDto
            {
                Id = 2,
                Name = "Men"
            }
        };

        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ReturnsAsync(categories);
        
        // Act
        var result = await _handler.Handle();
        
        // Assert
        result.Should().BeOfType<Ok<List<CategoryDto>>>();
    }
    
    [Fact]
    public async Task Handler_Should_Return_NotFound_When_Categories_Returns_Null()
    {
        // Arrange 
        
        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ReturnsAsync((List<CategoryDto>)null);
        
        // Act
        var result = await _handler.Handle();
        
        // Assert
        result.Should().BeOfType<NotFound>();
    }
    
    [Fact]
    public async Task Handler_Should_Return_NotFound_When_Categories_Are_Empty()
    {
        // Arrange 
        var emptyList = new List<CategoryDto>();
        
        _repositoryMock
            .Setup(repo => repo.GetAllCategoriesAsync())
            .ReturnsAsync(emptyList);
        
        // Act
        var result = await _handler.Handle();
        
        // Assert
        result.Should().BeOfType<NotFound>();
    }
}