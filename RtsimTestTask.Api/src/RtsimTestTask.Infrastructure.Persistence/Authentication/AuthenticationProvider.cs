using Microsoft.AspNetCore.Identity;
using RtsimTestTask.Core.Abstractions.Authentication;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Core.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DataMappers;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Authentication;

public class AuthenticationProvider(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    : IAuthenticationProvider
{
    public async Task<LoginResult> LoginAsync(LoginUserDto model, CancellationToken cancellationToken = default)
    {
        var signedUser = await signInManager.UserManager.FindByNameAsync(model.Username);
    
        if (signedUser is null)
            throw new UserNotFoundException(model.Username);
    
        var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
    
        if (result.Succeeded)
            return new SuccessLogin();
    
        return new FailedLogin();
    }
    
    public async Task<RegisterResult> RegisterAsync(User userData, string password, CancellationToken ct = default)
    {
        var userEntity = userData.MapToEntity();
        var result = await userManager.CreateAsync(userEntity, password);
        return result.Succeeded
            ? new RegisterSuccess(Guid.Parse(userEntity.Id))
            : new RegisterFailed(result.Errors.Select(x => x.Description));
    }
    
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}