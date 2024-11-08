using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork

{
    private readonly Lazy<IOrganizationsRepository> _lazyOrganizationsRepository =
        new(() => new OrganizationsRepository(dbContext));

    private readonly Lazy<IUsersRepository> _lazyUsersRepository =
        new(() => new UsersRepository(dbContext));

    public IOrganizationsRepository OrganizationRepository => _lazyOrganizationsRepository.Value;
    public IUsersRepository UserRepository => _lazyUsersRepository.Value;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}