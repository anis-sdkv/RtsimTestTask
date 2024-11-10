using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Authentication;

public interface IAccountManager
{
    Task<string> LoginAsync(LoginUserDto loginData, CancellationToken cancellationToken);
    Task<Guid> RegisterAsync(RegisterUserDto registerData, string role, CancellationToken cancellationToken);
    Task UpdateAccountData(DomainUser userNewData, CancellationToken cancellationToken);
}