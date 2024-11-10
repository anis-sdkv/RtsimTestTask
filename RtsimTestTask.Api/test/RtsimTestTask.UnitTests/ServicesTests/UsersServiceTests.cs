// using Moq;
// using RtsimTestTask.Core.Abstractions.Repositories;
// using RtsimTestTask.Core.DomainEntities;
// using RtsimTestTask.Core.Services;
// using RtsimTestTask.TestingInfrastructure.Fakers;
//
// namespace RtsimTestTask.UnitTests.ServicesTests;
//
// public class UsersServiceTests
// {
//     private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//     private readonly UsersService _usersService;
//     private readonly CancellationToken _cancellationToken;
//
//     public UsersServiceTests()
//     {
//         _unitOfWorkMock = new Mock<IUnitOfWork>();
//         _usersService = new UsersService(_unitOfWorkMock.Object);
//         _cancellationToken = CancellationToken.None;
//     }
//     //
//     // [Fact]
//     // public async Task GetByIdAsync_ShouldReturnUser()
//     // {
//     //     // Arrange
//     //     var userId = Guid.NewGuid();
//     //     var expectedUser = UserFaker.Generate() with { Id = userId };
//     //     _unitOfWorkMock.Setup(u => u.UserRepository.GetAsync(userId, _cancellationToken))
//     //         .ReturnsAsync(expectedUser);
//     //
//     //     // Act
//     //     var user = await _usersService.GetByIdAsync(userId, _cancellationToken);
//     //
//     //     // Assert
//     //     Assert.Equal(expectedUser, user);
//     //     _unitOfWorkMock.Verify(
//     //         u => u.UserRepository.GetAsync(userId, _cancellationToken),
//     //         Times.Once);
//     // }
//     //
//     // [Fact]
//     // public async Task GetAllAsync_ShouldReturnAllUsers()
//     // {
//     //     // Arrange
//     //     var expectedUsers = Enumerable.Range(1, Random.Shared.Next(1, 5)).Select(_ => UserFaker.Generate()).ToArray();
//     //     _unitOfWorkMock.Setup(u => u.UserRepository.GetAllAsync(_cancellationToken))
//     //         .ReturnsAsync(expectedUsers);
//     //
//     //     // Act
//     //     var users = await _usersService.GetAllAsync(_cancellationToken);
//     //
//     //     // Assert
//     //     Assert.Equal(expectedUsers, users);
//     //     _unitOfWorkMock.Verify(
//     //         u => u.UserRepository.GetAllAsync(_cancellationToken),
//     //         Times.Once);
//     // }
//     //
//     // [Fact]
//     // public async Task UpdateUserProfileAsync_ShouldUpdateUser()
//     // {
//     //     // Arrange
//     //     var user = UserFaker.Generate();
//     //     _unitOfWorkMock.Setup(u => u.UserRepository.UpdateAsync(user, _cancellationToken))
//     //         .Returns(Task.CompletedTask);
//     //
//     //     // Act
//     //     await _usersService.UpdateUserProfileAsync(user, _cancellationToken);
//     //
//     //     // Assert
//     //     _unitOfWorkMock.Verify(
//     //         u => u.UserRepository.UpdateAsync(user, _cancellationToken),
//     //         Times.Once);
//     // }
// }