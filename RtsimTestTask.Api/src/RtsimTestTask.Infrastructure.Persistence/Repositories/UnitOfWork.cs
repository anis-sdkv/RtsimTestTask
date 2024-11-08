using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork

{
    private readonly Lazy<IOrganizationsRepository> _lazyCommentRepository =
        new(() => new BaseRepository<Organization, Guid?>(dbContext));

    public IOrganizationsRepository OrganizationRepository => _lazyCommentRepository.Value;
    public IUsersRepository UserRepository { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}