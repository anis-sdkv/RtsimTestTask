// using AutoBogus;
// using Bogus;
// using Moq;
// using RtsimTestTask.Core.Abstractions.Repositories;
// using RtsimTestTask.Core.DataTransferObjects;
// using RtsimTestTask.Core.DataTransferObjects.RequestModels;
// using RtsimTestTask.Core.DomainEntities;
// using RtsimTestTask.Core.Services;
// using RtsimTestTask.TestingInfrastructure.Fakers;
//
// namespace RtsimTestTask.UnitTests.ServicesTests;
//
// public class OrganizationsServiceTests
// {
//     private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//     private readonly OrganizationsService _organizationsService;
//     private readonly CancellationToken _cancellationToken = CancellationToken.None;
//
//     public OrganizationsServiceTests()
//     {
//         _unitOfWorkMock = new Mock<IUnitOfWork>();
//         _organizationsService = new OrganizationsService(_unitOfWorkMock.Object);
//     }
//
//     [Fact]
//     public async Task CreateOrganizationAsync_ShouldAddOrganization()
//     {
//         // Arrange
//         var organization = new Faker<CreateOrganizationDto>().Generate();
//         _unitOfWorkMock.Setup(u => u.OrganizationRepository.Create(organization, _cancellationToken));
//         _unitOfWorkMock.Setup(u => u.SaveChangesAsync(_cancellationToken)).Returns(Task.FromResult(1));
//
//         // Act
//         await _organizationsService.CreateOrganizationAsync(organization, _cancellationToken);
//
//         // Assert
//         _unitOfWorkMock.Verify(u => u.OrganizationRepository.Create(organization, _cancellationToken), Times.Once);
//         _unitOfWorkMock.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
//     }
//
//     [Fact]
//     public async Task GetOrganizationByIdAsync_ShouldReturnOrganization()
//     {
//         // Arrange
//         var organizationId = Guid.NewGuid();
//         var expectedOrganization = OrganizationFaker.Generate().WithId(organizationId);
//         _unitOfWorkMock.Setup(u => u.OrganizationRepository.GetAsync(organizationId, _cancellationToken))
//             .ReturnsAsync(expectedOrganization);
//
//         // Act
//         var organization = await _organizationsService.GetOrganizationByIdAsync(organizationId, _cancellationToken);
//
//         // Assert
//         Assert.Equal(expectedOrganization, organization);
//         _unitOfWorkMock.Verify(
//             u => u.OrganizationRepository.GetAsync(organizationId, _cancellationToken),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task SearchOrganizationsAsync_ShouldReturnOrganizations()
//     {
//         // Arrange
//         var searchParams = new AutoFaker<SearchOrganizationDto>().Generate();
//         var expectedOrganizations = OrganizationFaker.Generate(Random.Shared.Next(1, 3)).ToArray();
//         _unitOfWorkMock.Setup(u => u.OrganizationRepository.SearchAsync(searchParams, _cancellationToken))
//             .ReturnsAsync(expectedOrganizations);
//
//         // Act
//         var organizations = await _organizationsService.SearchOrganizationsAsync(searchParams, _cancellationToken);
//
//         // Assert
//         Assert.Equal(expectedOrganizations, organizations);
//         _unitOfWorkMock.Verify(
//             u => u.OrganizationRepository.SearchAsync(searchParams, _cancellationToken),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task GetUsersByOrganizationIdAsync_ShouldReturnUsers()
//     {
//         // Arrange
//         var organizationId = Guid.NewGuid();
//         var expectedUsers = Enumerable.Range(0, 3)
//             .Select(_ => UserFaker.Generate() with { OrganizationId = organizationId })
//             .ToArray();
//         _unitOfWorkMock.Setup(u => u.UserRepository.GetUsersByOrganization(organizationId, _cancellationToken))
//             .ReturnsAsync(expectedUsers);
//
//         // Act
//         var users = await _organizationsService.GetUsersByOrganizationIdAsync(organizationId, _cancellationToken);
//
//         // Assert
//         Assert.Equal(expectedUsers, users);
//         _unitOfWorkMock.Verify(
//             u => u.UserRepository.GetUsersByOrganization(organizationId, _cancellationToken),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task UpdateOrganizationAsync_ShouldUpdateOrganizationAndSaveChanges()
//     {
//         // Arrange
//         var organization = OrganizationFaker.Generate();
//         _unitOfWorkMock.Setup(u => u.OrganizationRepository.UpdateAsync(organization, _cancellationToken))
//             .Returns(Task.CompletedTask);
//         _unitOfWorkMock.Setup(u => u.SaveChangesAsync(_cancellationToken))
//             .Returns(Task.FromResult(1));
//
//         // Act
//         await _organizationsService.UpdateOrganizationAsync(organization, _cancellationToken);
//
//         // Assert
//         _unitOfWorkMock.Verify(
//             u => u.OrganizationRepository.UpdateAsync(organization, _cancellationToken),
//             Times.Once);
//         _unitOfWorkMock.Verify(
//             u => u.SaveChangesAsync(_cancellationToken),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task DeleteOrganizationAsync_ShouldRemoveOrganizationAndSaveChanges()
//     {
//         // Arrange
//         var organizationId = Guid.NewGuid();
//         _unitOfWorkMock.Setup(u => u.OrganizationRepository.RemoveAsync(organizationId, _cancellationToken))
//             .Returns(Task.CompletedTask);
//         _unitOfWorkMock.Setup(u => u.SaveChangesAsync(_cancellationToken))
//             .Returns(Task.FromResult(1));
//
//         // Act
//         await _organizationsService.DeleteOrganizationAsync(organizationId, _cancellationToken);
//
//         // Assert
//         _unitOfWorkMock.Verify(
//             u => u.OrganizationRepository.RemoveAsync(organizationId, _cancellationToken),
//             Times.Once);
//         _unitOfWorkMock.Verify(
//             u => u.SaveChangesAsync(_cancellationToken),
//             Times.Once);
//     }
// }