namespace Ecommerce.Tests.Features.Products.GetAllBrands;

using Api.Domain.Brand;
using Api.Features.Products.GetAllBrandsWithCount;
using Api.Infrastructure.Repositories.FilterRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class GetAllBrandsTests
{
    private readonly GetAllBrandsWithCountHandler _handler;
    private readonly Mock<IFilterRepository> _repositoryMock;

    public GetAllBrandsTests()
    {
        _repositoryMock = new Mock<IFilterRepository>();
        _handler = new GetAllBrandsWithCountHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_Should_Return_200_Ok_With_Brands()
    {
        // Arrange
        var brands = new List<BrandDto>
        {
            new BrandDto
            {
                Id = 1,
                Name = "Polo",
                ProductCount = 3
            },
            new BrandDto
            {
                Id = 2,
                Name = "Nike",
                ProductCount = 4
            }
        };

        _repositoryMock
            .Setup(repo => repo.GetAllBrandsAsync())
            .ReturnsAsync(brands);
        
        // Act
        var result = await _handler.Handle();
        
        // Assert
        result.Should().BeOfType<Ok<List<BrandDto>>>();
    }
    
    [Fact]
    public async Task Handler_Should_Return_NotFound_When_Empty_List()
    {
        // Arrange
        var emptyList = new List<BrandDto>();

        _repositoryMock
            .Setup(repo => repo.GetAllBrandsAsync())
            .ReturnsAsync(emptyList);
        
        // Act
        var result = await _handler.Handle();
        
        // Assert
        result.Should().BeOfType<NotFound>();
    }
}