using FluentAssertions;
using Moq;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Domain.Services;
using RtsimTestTask.TestingInfrastructure.Fakers;

namespace RtsimTestTask.UnitTests.ServicesTests;

public class UsersServiceTests
{
    private readonly Mock<IUsersRepository> _mockUsersRepository;
    private readonly UsersService _usersService;
    private readonly CancellationToken _cancellationToken;

    public UsersServiceTests()
    {
        _mockUsersRepository = new Mock<IUsersRepository>();
        _usersService = new UsersService(_mockUsersRepository.Object);
        _cancellationToken = CancellationToken.None;
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ShouldReturnUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = UserFaker.Generate() with { Id = userId };
        _mockUsersRepository
            .Setup(r => r.GetUserByIdAsync(userId, _cancellationToken))
            .ReturnsAsync(expectedUser);

        // Act
        var user = await _usersService.GetByIdAsync(userId, _cancellationToken);

        // Assert
        user.Should().BeEquivalentTo(expectedUser);
        _mockUsersRepository.Verify(
            r => r.GetUserByIdAsync(userId, _cancellationToken),
            Times.Once);
    }


    [Fact]
    public async Task GetByIdAsync_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var setup = _mockUsersRepository
            .Setup(r => r.GetUserByIdAsync(userId, _cancellationToken))!
            .ReturnsAsync((DomainUser?)null);

        // Act
        var user = await _usersService.GetByIdAsync(userId, _cancellationToken);

        // Assert
        user.Should().BeNull();
        _mockUsersRepository.Verify(
            r => r.GetUserByIdAsync(userId, _cancellationToken),
            Times.Once);
    }
}