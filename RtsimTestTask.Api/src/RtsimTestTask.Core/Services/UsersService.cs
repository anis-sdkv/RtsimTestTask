using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.Abstractions.Services;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Services;

public class UsersService(IUnitOfWork unitOfWork) : IUsersService
{
    public Task<DomainUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetAsync(userId, cancellationToken);
    }

    public Task<IEnumerable<DomainUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetAllAsync(cancellationToken);
    }

    public Task UpdateUserProfileAsync(DomainUser domainUser, CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.UpdateAsync(domainUser, cancellationToken);
    }
}