using Microsoft.AspNetCore.Identity;
using RtsimTestTask.Core.Abstractions.Authentication;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Core.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DataMappers;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Authentication;

public class AuthenticationProvider(SignInManager<UserEntity> signInManager)
    : IAuthenticationProvider
{
    public async Task<Guid> LoginAsync(LoginUserDto loginData, CancellationToken cancellationToken = default)
    {
        var signedUser = await signInManager.UserManager.FindByNameAsync(loginData.Username);

        if (signedUser is null)
            throw new UserNotFoundException(loginData.Username);

        var result = await signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, false, false);

        if (result.Succeeded) return Guid.Parse(signedUser.Id);
        throw new LoginException();
    }

    public async Task<Guid> RegisterAsync(RegisterUserDto registerData, CancellationToken cancellationToken)
    {
        var userEntity = registerData.CreateUser();
        var result = await signInManager.UserManager.CreateAsync(userEntity, registerData.Password);
        if (!result.Succeeded)
            throw new RegistrationException(result.Errors.Select(x => x.Description));
        return Guid.Parse(userEntity.Id);
    }


    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}