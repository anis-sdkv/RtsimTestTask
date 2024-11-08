using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IRepository<TEntity, in TKey>
    where TEntity : DomainEntity<TKey>
{
    Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    void Add(TEntity entity, CancellationToken cancellationToken);
    Task RemoveAsync(TKey entity, CancellationToken cancellationToken);
}