using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Services;

public interface IUsersService
{
    Task<DomainUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<DomainUser>> GetAllAsync(CancellationToken cancellationToken);

    Task UpdateUserProfileAsync(DomainUser domainUser, CancellationToken cancellationToken);
}