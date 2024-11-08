using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.Abstractions.Services;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Services;

public class UsersService(IUnitOfWork unitOfWork) : IUsersService
{
    public Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetAsync(userId, cancellationToken);
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetAllAsync(cancellationToken);
    }

    public Task UpdateUserProfileAsync(User user, CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }
}