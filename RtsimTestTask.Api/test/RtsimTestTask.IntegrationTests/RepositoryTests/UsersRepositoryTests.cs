using FluentAssertions;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Repositories;
using RtsimTestTask.IntegrationTests.Fixtures;
using RtsimTestTask.TestingInfrastructure.Fakers;

namespace RtsimTestTask.IntegrationTests.RepositoryTests;

[Collection(nameof(TestFixture))]
public class UsersRepositoryTests
{
    private readonly TestFixture _fixture;
    private readonly IUsersRepository _usersRepository;
    private readonly IOrganizationsRepository _organizationsRepository;

    public UsersRepositoryTests(TestFixture fixture)
    {
        _fixture = fixture;
        _usersRepository = _fixture.UsersRepository;
        _organizationsRepository = _fixture.OrganizationsRepository;
    }

    [Fact]
    public async Task GetUsersByOrganizationAsync_ValidOrganizationId_ReturnsOnlyUsersInOrganization()
    {
        // Arrange
        var targetOrganizationId =
            (await _organizationsRepository.CreateAsync(OrganizationFaker.GenerateDto(), CancellationToken.None));

        var anotherOrganizationId =
            (await _organizationsRepository.CreateAsync(OrganizationFaker.GenerateDto(), CancellationToken.None));


        var usersInOrganization = Enumerable
            .Range(0, 3)
            .Select(_ => UserFaker.Generate() with { OrganizationId = targetOrganizationId })
            .ToList();
        var otherUsers = Enumerable
            .Range(0, 20)
            .Select(_ => UserFaker.Generate() with { OrganizationId = anotherOrganizationId });

        await _fixture.Seed(usersInOrganization.Concat(otherUsers));

        // Act
        var result = (await _usersRepository.GetUsersByOrganizationAsync(targetOrganizationId, CancellationToken.None))
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(usersInOrganization.Count);
        result.Should().OnlyContain(user => user.OrganizationId == targetOrganizationId);
    }

    [Fact]
    public async Task GetUsersByOrganizationAsync_InvalidOrganizationId_ReturnsEmptyList()
    {
        // Arrange
        var validId =
            (await _organizationsRepository.CreateAsync(OrganizationFaker.GenerateDto(), CancellationToken.None));


        var usersInOtherOrganizations = Enumerable
            .Range(0, 3)
            .Select(_ => UserFaker.Generate() with { OrganizationId = validId })
            .ToList();

        await _fixture.Seed(usersInOtherOrganizations);

        // Act
        var result = (await _usersRepository.GetUsersByOrganizationAsync(Guid.NewGuid(), CancellationToken.None))
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserByIdAsync_ValidUserId_ReturnsUser()
    {
        // Arrange
        var validId =
            (await _organizationsRepository.CreateAsync(OrganizationFaker.GenerateDto(), CancellationToken.None));

        var user = UserFaker.Generate() with { OrganizationId = validId };
        await _fixture.Seed([user]);

        // Act
        var result = await _usersRepository.GetUserByIdAsync(user.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_InvalidUserId_ReturnsNull()
    {
        // Act
        var result = await _usersRepository.GetUserByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}