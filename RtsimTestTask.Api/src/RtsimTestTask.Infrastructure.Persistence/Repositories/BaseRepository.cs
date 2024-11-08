using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Core;
using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Core.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DbContext;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity, TKey>(ApplicationDbContext dbContext) where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken) =>
        await _dbSet.FindAsync([id], cancellationToken) ?? throw new EntityNotFoundException(nameof(TEntity));

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

    public TEntity Add(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Add(entity);
    }

    public async Task RemoveAsync(TKey id, CancellationToken cancellationToken)
    {
        var user = await _dbSet.FindAsync([id], cancellationToken);
        if (user == null) throw new EntityNotFoundException(nameof(TEntity));
        Remove(user);
    }


    public async Task UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken)
    {
        var dbEntity = await _dbSet.FindAsync([id], cancellationToken) ??
                       throw new EntityNotFoundException(nameof(TEntity));
        dbContext.Entry(dbEntity).CurrentValues.SetValues(entity);
    }

    private void Remove(TEntity entity)
    {
        _dbSet.Entry(entity).State = EntityState.Detached;
        _dbSet.Remove(entity);
    }
}