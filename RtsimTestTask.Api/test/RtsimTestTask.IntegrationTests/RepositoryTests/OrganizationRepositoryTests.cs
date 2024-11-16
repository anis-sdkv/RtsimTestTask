using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Options;
using RtsimTestTask.Infrastructure.Persistence.Repositories;
using RtsimTestTask.IntegrationTests.Fixtures;
using RtsimTestTask.TestingInfrastructure.Fakers;
using Xunit.Abstractions;

namespace RtsimTestTask.IntegrationTests.RepositoryTests;

[Collection(nameof(TestFixture))]
public class OrganizationRepositoryTests
{
    private const int InfinityPageConstant = Int32.MaxValue;
    private readonly TestFixture _fixture;
    private readonly ITestOutputHelper _outputHelper;

    private readonly ApplicationDbContext _context;
    private readonly IOrganizationsRepository _organizationsRepository;

    public OrganizationRepositoryTests(TestFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        _outputHelper = outputHelper;
        _context = fixture.Context;
        _organizationsRepository = fixture.OrganizationsRepository;
    }


    [Fact]
    public async Task SearchOrganizationsAsync_EmptyFilters_ReturnsAllOrganizations()
    {
        // Arrange
        var count = 1;
        var organizations = OrganizationFaker.Generate(count)
            .Select(x => x with { CreatedAt = DateTime.UtcNow.AddDays(-10) })
            .ToList();
        await _fixture.Seed(organizations);
        var searchParams = new SearchOrganizationDto(0, InfinityPageConstant);

        // Act
        var result = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().BeGreaterOrEqualTo(count);
        result.Select(x => x.OrganizationName).Should()
            .IntersectWith(organizations.Select(x => x.OrganizationName));
    }

    [Fact]
    public async Task SearchOrganizationsAsync_WithQueryParams_ReturnsFilteredOrganizations()
    {
        // Arrange  
        var organizations = new List<DomainOrganization>
        {
            OrganizationFaker.Generate() with { OrganizationName = "Alpha" },
            OrganizationFaker.Generate() with { OrganizationName = "Beta" },
            OrganizationFaker.Generate() with { OrganizationName = "Alpha1" },
            OrganizationFaker.Generate() with { OrganizationName = "Gamma" },
            OrganizationFaker.Generate() with { OrganizationName = "Alpha2" },
        };
        await _fixture.Seed(organizations);

        var searchParams = new SearchOrganizationDto(
            Page: 0,
            PageSize: 10,
            QueryParams: new SearchOrganizationDto.QueryParameters("OrganizationName", "Alp")
        );
        var expectedSet = organizations
            .Where(x => x.OrganizationName.StartsWith("Alp"))
            .Select(x => x.OrganizationName)
            .ToList();

        // Act
        var result = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .Select(x => x.OrganizationName)
            .ToList();

        // Assert
        result.Count.Should().BeGreaterOrEqualTo(3);
        result.Should().IntersectWith(expectedSet);
    }

    [Theory]
    [InlineData(true, true, 5)]
    [InlineData(true, false, 10)]
    [InlineData(false, true, 15)]
    [InlineData(false, false, 20)]
    public async Task SearchOrganizationsAsync_WithDateFilters_ReturnsFilteredOrganizations(
        bool after,
        bool before,
        int expectedCount)
    {
        // Arrange

        var organizations = Enumerable
            .Range(0, 20)
            .Select(days => OrganizationFaker.Generate() with { CreatedAt = DateTime.UtcNow.AddDays(-days) })
            .ToList();
        await _fixture.Seed(organizations);

        var searchParams = new SearchOrganizationDto(
            Page: 0,
            PageSize: 20,
            DateFilterParams: new SearchOrganizationDto.DateFilterParameters(
                CreatedAfter: after ? DateTime.UtcNow.AddDays(-10) : null,
                CreatedBefore: before ? DateTime.UtcNow.AddDays(-5) : null
            )
        );

        // Act
        var result = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .ToList();

        // Assert
        result.Count.Should().BeGreaterOrEqualTo(expectedCount);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SearchOrganizationsAsync_WithSortParams_ReturnsSortedOrganizations(bool byDescending)
    {
        // Arrange
        var count = 20;
        var uniquePostfix = $"_{(byDescending ? "desc" : "asc")}";
        var sortedSource = Enumerable
            .Range(0, count)
            .Select(i => OrganizationFaker.Generate() with
            {
                OrganizationName = $"org{(char)('A' + i)}{uniquePostfix}"
            })
            .ToArray();

        var randomOrdered = new DomainOrganization[count];
        sortedSource.CopyTo(randomOrdered, 0);
        while (sortedSource.SequenceEqual(randomOrdered))
            Random.Shared.Shuffle(randomOrdered);

        randomOrdered.Should().NotContainInOrder(sortedSource);

        await _fixture.Seed(randomOrdered);

        var searchParams = new SearchOrganizationDto(
            Page: 0,
            PageSize: InfinityPageConstant,
            SortParams: new SearchOrganizationDto.SortParameters("OrganizationName", byDescending)
        );

        var expectedOrder = (byDescending
                ? sortedSource.OrderByDescending(x => x.OrganizationName)
                : sortedSource.AsEnumerable())
            .Select(x => x.OrganizationName)
            .ToArray();

        // Act
        var result = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .Select(x => x.OrganizationName)
            .ToArray();

        // Assert
        result.Should().ContainInOrder(expectedOrder);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(0, 10)]
    [InlineData(1, 5)]
    [InlineData(1, 10)]
    [InlineData(1, 15)]
    public async Task SearchOrganizationsAsync_PaginationParams_ReturnsCorrectPaginatedResults(int page, int pageSize)
    {
        // Arrange
        var count = 20;
        var uniqueTestPrefix = $"_page_{page}_{pageSize}";
        var source = OrganizationFaker.Generate(count)
            .Select(x => x with { OrganizationName = x.OrganizationName + uniqueTestPrefix })
            .OrderBy(x => x.OrganizationName)
            .ToArray();
        await _fixture.Seed(source);
        var searchParams = new SearchOrganizationDto(
            page,
            pageSize,
            new SearchOrganizationDto.QueryParameters("OrganizationName", uniqueTestPrefix),
            new SearchOrganizationDto.SortParameters("OrganizationName")
        );

        var expectedCollection = source
            .Skip(pageSize * page)
            .Take(pageSize)
            .Select(x => x.OrganizationName)
            .ToArray();

        // Act
        var resultCollection = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .Select(x => x.OrganizationName)
            .ToArray();

        // Assert
        resultCollection.Should().Contain(expectedCollection);
    }

    [Fact]
    public async Task SearchOrganizationsAsync_WithEmptyPage_ReturnsEmptyResult()
    {
        var count = 20;
        var source = OrganizationFaker.Generate(count)
            .ToArray();
        await _fixture.Seed(source);
        var searchParams = new SearchOrganizationDto(0, 0);

        var resultCollection = (await _organizationsRepository.SearchAsync(searchParams, CancellationToken.None))
            .ToArray();

        resultCollection.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllOrganizations()
    {
        // Arrange
        var uniqueTestPostfix = "_getAll";
        var count = 10;
        var organizations = OrganizationFaker.Generate(count)
            .Select(x => x with { OrganizationName = x.OrganizationName + uniqueTestPostfix })
            .ToList();
        await _fixture.Seed(organizations);

        // Act
        var result = (await _organizationsRepository.GetAllAsync(CancellationToken.None))
            .Where(x => x.OrganizationName.EndsWith(uniqueTestPostfix))
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(count);
        result.Select(x => x.OrganizationName).Should()
            .Contain(organizations.Select(x => x.OrganizationName));
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsCorrectOrganization()
    {
        // Arrange
        var organization = OrganizationFaker.Generate();
        await _fixture.Seed([organization]);

        // Act
        var result = await _organizationsRepository.GetByIdAsync(organization.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.OrganizationName.Should().Be(organization.OrganizationName);
        result!.Id.Should().Be(organization.Id);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        // Act
        var result = await _organizationsRepository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByNameAsync_ValidName_ReturnsCorrectOrganization()
    {
        // Arrange
        var organization = OrganizationFaker.Generate();
        await _fixture.Seed([organization]);

        // Act
        var result =
            await _organizationsRepository.GetByNameAsync(organization.OrganizationName, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(organization.Id);
    }

    [Fact]
    public async Task GetByNameAsync_InvalidName_ReturnsNull()
    {
        // Act
        var result =
            await _organizationsRepository.GetByNameAsync("NonExistentName_" + Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ValidData_UpdatesOrganization()
    {
        // Arrange
        var newName = "UpdatedName_updateTest";
        var organization = OrganizationFaker.Generate();
        await _fixture.Seed([organization]);
        var updateDto = new UpdateOrganizationDto(
            organization.Id,
            newName,
            organization.Address,
            organization.PhoneNumber
        );

        // Act
        await _organizationsRepository.UpdateAsync(updateDto, CancellationToken.None);
        var updatedOrganization = await _organizationsRepository.GetByIdAsync(organization.Id, CancellationToken.None);

        // Assert
        updatedOrganization.Should().NotBeNull();
        updatedOrganization!.OrganizationName.Should().Be(newName);
    }

    [Fact]
    public async Task CreateAsync_ValidData_ReturnsNewOrganizationId()
    {
        // Arrange
        var createDto = OrganizationFaker.GenerateDto();

        // Act
        var newId = await _organizationsRepository.CreateAsync(createDto, CancellationToken.None);
        var createdOrganization = await _organizationsRepository.GetByIdAsync(newId, CancellationToken.None);

        // Assert
        createdOrganization.Should().NotBeNull();
        createdOrganization!.OrganizationName.Should().Be(createDto.OrganizationName);
        createdOrganization.Address.Should().Be(createDto.Address);
        createdOrganization.PhoneNumber.Should().Be(createDto.PhoneNumber);
    }

    [Fact]
    public async Task RemoveAsync_ValidId_RemovesOrganization()
    {
        // Arrange
        var organization = OrganizationFaker.Generate();
        await _fixture.Seed([organization]);

        // Act
        await _organizationsRepository.RemoveAsync(organization.Id, CancellationToken.None);
        var deletedOrganization = await _organizationsRepository.GetByIdAsync(organization.Id, CancellationToken.None);

        // Assert
        deletedOrganization.Should().BeNull();
    }
}