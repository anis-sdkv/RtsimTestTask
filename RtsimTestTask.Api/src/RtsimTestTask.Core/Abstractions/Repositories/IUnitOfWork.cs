using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IUnitOfWork
{
    IOrganizationsRepository OrganizationRepository { get; }
    IUsersRepository UserRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}