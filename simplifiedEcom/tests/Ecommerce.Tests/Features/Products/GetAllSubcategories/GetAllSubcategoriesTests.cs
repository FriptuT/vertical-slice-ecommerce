namespace Ecommerce.Tests.Features.Products.GetAllSubcategories;

using Api.Domain.Category;
using Api.Features.Products.GetAllSubcategories;
using Api.Infrastructure.Repositories.FilterRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetAllSubcategoriesTests
{
    private readonly Mock<IFilterRepository> _repositoryMock;
    private readonly GetAllSubcategoriesHandler _handler;

    public GetAllSubcategoriesTests()
    {
        _repositoryMock = new Mock<IFilterRepository>();
        _handler = new GetAllSubcategoriesHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_200_Ok_With_Subcategories()
    {
        // Arrange
        var categories = new List<SubcategoryDto>
        {
            new SubcategoryDto
            {
                Id = 1,
                Name = "Dress"
            },
            new SubcategoryDto
            {
                Id = 2,
                Name = "Top"
            }
        };

        var categoryId = 1;
        
        _repositoryMock
            .Setup(repo => repo.GetByCategoryIdAsync(categoryId))
            .ReturnsAsync(categories);
        
        // Act
        var result = await _handler.Handle(categoryId);
        
        // Assert
        result.Should().BeOfType<Ok<List<SubcategoryDto>>>();
    }
    
    
    [Fact]
    public async Task Handler_Should_Return_NotFound_When_Subcategories_Are_Empty()
    {
        // Arrange
        var emptyList = new List<SubcategoryDto>();

        var categoryId = 1;
        
        _repositoryMock
            .Setup(repo => repo.GetByCategoryIdAsync(categoryId))
            .ReturnsAsync(emptyList);
        
        // Act
        var result = await _handler.Handle(categoryId);
        
        // Assert
        result.Should().BeOfType<NotFound>();
    }
}