using AutoBogus;
using Bogus;
using FluentAssertions;
using Moq;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.Services;
using RtsimTestTask.TestingInfrastructure.Fakers;

namespace RtsimTestTask.UnitTests.ServicesTests;

public class OrganizationsServiceTests
{
    private readonly Mock<IUsersRepository> _mockUsersRepository;
    private readonly Mock<IOrganizationsRepository> _mockOrganizationsRepository;
    private readonly OrganizationsService _organizationsService;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public OrganizationsServiceTests()
    {
        _mockOrganizationsRepository = new Mock<IOrganizationsRepository>();
        _mockUsersRepository = new Mock<IUsersRepository>();
        _organizationsService =
            new OrganizationsService(_mockOrganizationsRepository.Object, _mockUsersRepository.Object);
    }

    [Fact]
    public async Task CreateOrganizationAsync_ShouldAddOrganization()
    {
        // Arrange
        var organization = new AutoFaker<CreateOrganizationDto>().Generate();
        _mockOrganizationsRepository.Setup(u => u.CreateAsync(organization, _cancellationToken));

        // Act
        await _organizationsService.CreateOrganizationAsync(organization, _cancellationToken);

        // Assert
        _mockOrganizationsRepository.Verify(u => u.CreateAsync(organization, _cancellationToken), Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetOrganizationByIdAsync_ShouldReturnOrganization()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var expectedOrganization = OrganizationFaker.Generate().WithId(organizationId);
        _mockOrganizationsRepository.Setup(u => u.GetByIdAsync(organizationId, _cancellationToken))
            .ReturnsAsync(expectedOrganization);

        // Act
        var organization = await _organizationsService.GetOrganizationByIdAsync(organizationId, _cancellationToken);

        // Assert
        organization.Should().BeEquivalentTo(expectedOrganization);
        _mockOrganizationsRepository.Verify(
            u => u.GetByIdAsync(organizationId, _cancellationToken),
            Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task SearchOrganizationsAsync_ShouldReturnOrganizations()
    {
        // Arrange
        var searchParams = new AutoFaker<SearchOrganizationDto>().Generate();
        var expectedOrganizations = OrganizationFaker.Generate(Random.Shared.Next(1, 3)).ToArray();
        _mockOrganizationsRepository.Setup(u => u.SearchAsync(searchParams, _cancellationToken))
            .ReturnsAsync(expectedOrganizations);

        // Act
        var organizations = await _organizationsService.SearchOrganizationsAsync(searchParams, _cancellationToken);

        // Assert
        organizations.Should().BeEquivalentTo(expectedOrganizations);
        _mockOrganizationsRepository.Verify(
            u => u.SearchAsync(searchParams, _cancellationToken),
            Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetUsersByOrganizationIdAsync_ShouldReturnUsers()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var expectedUsers = Enumerable.Range(0, 3)
            .Select(_ => UserFaker.Generate() with { OrganizationId = organizationId })
            .ToArray();
        _mockUsersRepository.Setup(u => u.GetUsersByOrganizationAsync(organizationId, _cancellationToken))
            .ReturnsAsync(expectedUsers);

        // Act
        var users = await _organizationsService.GetUsersByOrganizationIdAsync(organizationId, _cancellationToken);

        // Assert
        users.Should().BeEquivalentTo(expectedUsers);
        _mockUsersRepository.Verify(
            u => u.GetUsersByOrganizationAsync(organizationId, _cancellationToken),
            Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }


    [Fact]
    public async Task UpdateOrganizationAsync_ShouldUpdateOrganization()
    {
        // Arrange
        var organization = new AutoFaker<UpdateOrganizationDto>().Generate();
        _mockOrganizationsRepository.Setup(u => u.UpdateAsync(organization, _cancellationToken))
            .Returns(Task.CompletedTask);

        // Act
        await _organizationsService.UpdateOrganizationAsync(organization, _cancellationToken);

        // Assert
        _mockOrganizationsRepository.Verify(
            u => u.UpdateAsync(organization, _cancellationToken),
            Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task DeleteOrganizationAsync_ShouldRemoveOrganization()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        _mockOrganizationsRepository.Setup(u => u.RemoveAsync(organizationId, _cancellationToken))
            .Returns(Task.CompletedTask);

        // Act
        await _organizationsService.DeleteOrganizationAsync(organizationId, _cancellationToken);

        // Assert
        _mockOrganizationsRepository.Verify(
            u => u.RemoveAsync(organizationId, _cancellationToken),
            Times.Once);
        _mockOrganizationsRepository.VerifyNoOtherCalls();
    }
}