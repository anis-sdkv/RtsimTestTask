using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Domain;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Domain.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DbContext;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity, TKey>(ApplicationDbContext context) where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    protected async Task<TEntity> BaseGetAsync(TKey id, CancellationToken cancellationToken) =>
        await _dbSet.FindAsync([id], cancellationToken) ?? throw new EntityNotFoundException(nameof(TEntity));

    protected async Task<IEnumerable<TEntity>> BaseGetAllAsync(CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

    protected async Task<TEntity> BaseAdd(TEntity entity, CancellationToken cancellationToken)
    {
          _dbSet.Add(entity);
          await context.SaveChangesAsync(cancellationToken);
          return entity; 
    }

    protected async Task BaseRemoveAsync(TKey id, CancellationToken cancellationToken)
    {
        var user = await _dbSet.FindAsync([id], cancellationToken);
        if (user == null) throw new EntityNotFoundException(nameof(TEntity));
        _dbSet.Entry(user).State = EntityState.Detached;
        _dbSet.Remove(user);
    }


    protected async Task BaseUpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken)
    {
        var dbEntity = await _dbSet.FindAsync([id], cancellationToken) ??
                       throw new EntityNotFoundException(nameof(TEntity));
        context.Entry(dbEntity).CurrentValues.SetValues(entity);
    }
}