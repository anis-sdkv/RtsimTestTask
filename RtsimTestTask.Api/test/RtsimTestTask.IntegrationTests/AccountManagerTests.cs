using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.Constants;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.Authentication;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.IntegrationTests.Fixtures;
using RtsimTestTask.TestingInfrastructure.Fakers;

namespace RtsimTestTask.IntegrationTests;

[Collection(nameof(TestFixture))]
public class AccountManagerTests
{
    private readonly IOrganizationsRepository _organizationsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IAccountManager _accountManager;

    public AccountManagerTests(TestFixture fixture)
    {
        _usersRepository = fixture.UsersRepository;
        _organizationsRepository = fixture.OrganizationsRepository;
        var userManager = fixture.UserManager;
        Mock<IJwtProvider> mockJwtProvider = new();
        mockJwtProvider.Setup(p => p.GenerateToken(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
            .Returns("token");
        _accountManager = new AccountManager(
            userManager,
            mockJwtProvider.Object,
            fixture.Context,
            _organizationsRepository,
            fixture.TestMapper
        );
    }

    [Fact]
    public async Task RegisterAsync_WithExistingUsername_ShouldThrowUserAlreadyExistsException()
    {
        // Arrange
        var role = DomainRoles.User;
        var validOrganization = OrganizationFaker.GenerateDto();
        var orgId = await _organizationsRepository.CreateAsync(validOrganization, CancellationToken.None);
        var validRegisterDto = UserFaker.GenerateDto() with { OrganizationId = orgId };
        var user = await _accountManager.RegisterAsync(validRegisterDto, role, CancellationToken.None);

        var invalidRegisterData = UserFaker.GenerateDto() with { UserName = validRegisterDto.UserName };

        // Act
        var register = async () =>
            await _accountManager.RegisterAsync(invalidRegisterData, role, CancellationToken.None);

        // Assert
        await register.Should().ThrowExactlyAsync<UserAlreadyExistException>();
    }

    [Fact]
    public async Task RegisterAsync_ValidData_ShouldCreateNewUser()
    {
        // Arrange
        var role = DomainRoles.User;
        var validOrganization = OrganizationFaker.GenerateDto();
        var orgId = await _organizationsRepository.CreateAsync(validOrganization, CancellationToken.None);
        var validRegisterDto = UserFaker.GenerateDto() with { OrganizationId = orgId };

        // Act
        var registredUserId = await _accountManager.RegisterAsync(validRegisterDto, role, CancellationToken.None);
        var user = await _usersRepository.GetUserByIdAsync(registredUserId, CancellationToken.None);

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().Be(registredUserId);
        user.Username.Should().Be(validRegisterDto.UserName);
    }

    [Fact]
    public async Task RegisterAsync_InvalidOrganizationId_ShouldThrowOrganizationNotExistException()
    {
        var fakeOrganization = OrganizationFaker.Generate();
        var registerData = new RegisterUserDto("TestUser", "TestPassword123!", fakeOrganization.Id);
        var role = DomainRoles.User;

        // Act
        var register = async () => await _accountManager.RegisterAsync(registerData, role, CancellationToken.None);

        // Assert
        await register.Should().ThrowExactlyAsync<OrganizationNotExistException>();
    }
    
    [Fact]
    public async Task LoginAsync_ValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var role = DomainRoles.User;
        var organization = OrganizationFaker.GenerateDto();
        var orgId = await _organizationsRepository.CreateAsync(organization, CancellationToken.None);
        var registerDto = UserFaker.GenerateDto() with { OrganizationId = orgId };

        var userId = await _accountManager.RegisterAsync(registerDto, role, CancellationToken.None);

        var loginDto = new LoginUserDto(registerDto.UserName, registerDto.Password);

        // Act
        var token = await _accountManager.LoginAsync(loginDto, CancellationToken.None);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task LoginAsync_InvalidUsername_ShouldThrowUserNotFoundException()
    {
        // Arrange
        var loginDto = new LoginUserDto("NonExistingUser", "TestPassword123!");

        // Act
        var loginAttempt = async () => await _accountManager.LoginAsync(loginDto, CancellationToken.None);

        // Assert
        await loginAttempt.Should().ThrowExactlyAsync<UserNotFoundException>();
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ShouldThrowLoginException()
    {
        // Arrange
        var role = DomainRoles.User;
        var organization = OrganizationFaker.GenerateDto();
        var orgId = await _organizationsRepository.CreateAsync(organization, CancellationToken.None);
        var registerDto = UserFaker.GenerateDto() with { OrganizationId = orgId };

        var userId = await _accountManager.RegisterAsync(registerDto, role, CancellationToken.None);

        // Act
        var loginDto = new LoginUserDto(registerDto.UserName, "IncorrectPassword");
        var loginAttempt = async () => await _accountManager.LoginAsync(loginDto, CancellationToken.None);

        // Assert
        await loginAttempt.Should().ThrowExactlyAsync<LoginException>();
    }

}