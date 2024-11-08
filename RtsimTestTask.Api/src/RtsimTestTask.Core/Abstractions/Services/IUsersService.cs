using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Services;

public interface IUsersService
{
    Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task UpdateUserProfileAsync(User user, CancellationToken cancellationToken);
}