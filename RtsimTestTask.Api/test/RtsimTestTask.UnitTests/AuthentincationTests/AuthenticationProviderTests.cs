// using AutoBogus;
// using Microsoft.AspNetCore.Identity;
// using Moq;
// using RtsimTestTask.Core.DataTransferObjects.RequestModels;
// using RtsimTestTask.Core.DomainEntities;
// using RtsimTestTask.Core.Exceptions;
// using RtsimTestTask.Infrastructure.Persistence.Authentication;
// using RtsimTestTask.Infrastructure.Persistence.DataMappers;
// using RtsimTestTask.Infrastructure.Persistence.Entities;
// using RtsimTestTask.TestingInfrastructure.Fakers;
//
// namespace RtsimTestTask.UnitTests.AuthentincationTests;
//
// public class AuthenticationProviderTests
// {
//     private readonly Mock<UserManager<UserEntity>> _mockUserManager;
//     private readonly Mock<SignInManager<UserEntity>> _mockSignInManager;
//     private readonly AuthenticationProvider _authenticationProvider;
//
//     public AuthenticationProviderTests()
//     {
//         _mockUserManager = MockUserManager();
//         _mockSignInManager = MockSignInManager(_mockUserManager.Object);
//         _authenticationProvider = new AuthenticationProvider(_mockSignInManager.Object);
//     }
//
//     private Mock<UserManager<UserEntity>> MockUserManager()
//     {
//         var store = new Mock<IUserStore<UserEntity>>();
//         return new Mock<UserManager<UserEntity>>(store.Object, null, null, null, null, null, null, null, null);
//     }
//
//     private Mock<SignInManager<UserEntity>> MockSignInManager(UserManager<UserEntity> userManager)
//     {
//         var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
//         var claimsFactory = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
//         return new Mock<SignInManager<UserEntity>>(
//             userManager,
//             contextAccessor.Object,
//             claimsFactory.Object,
//             null,
//             null,
//             null,
//             null);
//     }
//
//     [Fact]
//     public async Task LoginAsync_ShouldReturnGuid_WhenLoginIsSuccessful()
//     {
//         // Arrange
//         var model = new AutoFaker<LoginUserDto>().Generate();
//         var userEntity = new UserEntity { Id = Guid.NewGuid().ToString(), UserName = model.Username };
//         var signInResult = SignInResult.Success;
//
//         _mockUserManager.Setup(m => m.FindByNameAsync(model.Username))
//             .ReturnsAsync(userEntity);
//         _mockSignInManager.Setup(m => m.PasswordSignInAsync(model.Username, model.Password, false, false))
//             .ReturnsAsync(signInResult);
//
//         // Act
//         var result = await _authenticationProvider.LoginAsync(model, CancellationToken.None);
//
//         // Assert
//         Assert.Equal(Guid.Parse(userEntity.Id), result);
//         _mockSignInManager.Verify(m => m.PasswordSignInAsync(model.Username, model.Password, false, false),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task LoginAsync_ShouldThrowUserNotFoundException_WhenUserNotFound()
//     {
//         // Arrange
//         var model = new AutoFaker<LoginUserDto>().Generate();
//
//         _mockUserManager.Setup(m => m.FindByNameAsync(model.Username))
//             .ReturnsAsync((UserEntity?)null);
//
//         // Act & Assert
//         await Assert.ThrowsAsync<UserNotFoundException>(() =>
//             _authenticationProvider.LoginAsync(model, CancellationToken.None));
//     }
//
//     [Fact]
//     public async Task LoginAsync_ShouldThrowLoginException_WhenSignInFails()
//     {
//         // Arrange
//         var model = new AutoFaker<LoginUserDto>().Generate();
//         var userEntity = new UserEntity { Id = Guid.NewGuid().ToString() };
//         var signInResult = SignInResult.Failed;
//
//         _mockUserManager.Setup(m => m.FindByNameAsync(model.Username))
//             .ReturnsAsync(userEntity);
//         _mockSignInManager.Setup(m => m.PasswordSignInAsync(model.Username, model.Password, false, false))
//             .ReturnsAsync(signInResult);
//
//         // Act & Assert
//         await Assert.ThrowsAsync<LoginException>(() =>
//             _authenticationProvider.LoginAsync(model, CancellationToken.None));
//     }
//
//     [Fact]
//     public async Task RegisterAsync_ShouldReturnGuid_WhenRegistrationIsSuccessful()
//     {
//         // Arrange
//         var registerData = new AutoFaker<RegisterUserDto>().Generate();
//
//         _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<UserEntity>(), registerData.Password))
//             .ReturnsAsync(IdentityResult.Success);
//
//         // Act
//         var guid = await _authenticationProvider.RegisterAsync(registerData, CancellationToken.None);
//
//         // Assert
//         Assert.NotEqual(default, guid);
//         _mockUserManager.Verify(
//             m => m.CreateAsync(It.IsAny<UserEntity>(), registerData.Password),
//             Times.Once);
//     }
//
//     [Fact]
//     public async Task RegisterAsync_ShouldThrowRegistrationException_WhenRegistrationFails()
//     {
//         // Arrange
//         var registerData = new AutoFaker<RegisterUserDto>().Generate();
//         var result = IdentityResult.Failed(new IdentityError { Description = "Error" });
//
//         _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<UserEntity>(), registerData.Password))
//             .ReturnsAsync(result);
//
//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<RegistrationException>(() =>
//             _authenticationProvider.RegisterAsync(registerData, CancellationToken.None));
//
//         _mockUserManager.Verify(
//             m => m.CreateAsync(It.IsAny<UserEntity>(), registerData.Password),
//             Times.Once);
//         Assert.Contains("Error", exception.Errors);
//     }
//
//     [Fact]
//     public async Task LogoutAsync_ShouldCallSignOutAsync()
//     {
//         // Act
//         await _authenticationProvider.LogoutAsync();
//
//         // Assert
//         _mockSignInManager.Verify(m => m.SignOutAsync(), Times.Once);
//     }
// }