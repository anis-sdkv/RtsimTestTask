using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Authentication;

public interface IAuthenticationProvider
{
    Task<OperationResult<Guid>> LoginAsync(LoginUserDto dto, CancellationToken cancellationToken);
    Task<OperationResult<Guid>> RegisterAsync(User userData, string password, CancellationToken cancellationToken);
    Task<OperationResult> LogoutAsync();
}