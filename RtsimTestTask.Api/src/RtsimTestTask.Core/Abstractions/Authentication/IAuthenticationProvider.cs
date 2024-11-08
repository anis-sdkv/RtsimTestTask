using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Authentication;

public interface IAuthenticationProvider
{
    Task<Guid> LoginAsync(LoginUserDto loginData, CancellationToken cancellationToken);
    Task<Guid> RegisterAsync(RegisterUserDto registerData, CancellationToken cancellationToken);
    Task LogoutAsync();
}