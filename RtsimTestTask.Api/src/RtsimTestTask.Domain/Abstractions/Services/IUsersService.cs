using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Services;

public interface IUsersService
{
    Task<DomainUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}