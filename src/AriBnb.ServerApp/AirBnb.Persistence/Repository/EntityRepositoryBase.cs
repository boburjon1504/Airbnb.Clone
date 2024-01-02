using System.Linq.Expressions;
using AirBnb.Domain.Common.Cashing;
using AirBnb.Domain.Common.Entities;
using AirBnb.Persistence.Cashing.Brokers;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Persistence.Repository;

public class EntityRepositoryBase<TEntity, TContext>(TContext context, ICasheBroker casheBroker,
    CasheEntryOptions? casheEntryOptions = default)
        where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => context;

    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(enity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var foundEntity = default(TEntity?);

        if (casheEntryOptions is null || !await casheBroker.TryGetAsync(id.ToString(), out TEntity? cashedEntity))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();
            if (asNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
            if (foundEntity is not null && casheEntryOptions is not null)
                await casheBroker.SetAsync(foundEntity.Id.ToString(), foundEntity, casheEntryOptions);
        }
        else
        {
            foundEntity = cashedEntity;
        }

        return foundEntity;
    }

    protected async ValueTask<IList<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        initialQuery = initialQuery.Where(entity => ids.Contains(entity.Id));

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken: cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        if (casheEntryOptions is not null)
            await casheBroker.SetAsync(entity.Id.ToString(), entity, casheEntryOptions);

        return entity;
    }

    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        if (casheEntryOptions is not null)
            await casheBroker.SetAsync(entity.Id.ToString(), entity, casheEntryOptions);

        return entity;
    }

    protected async ValueTask<TEntity> DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        if (casheEntryOptions is not null)
            await casheBroker.DeleteAsync<TEntity>(entity.Id.ToString());

        return entity;
    }


    protected async ValueTask<TEntity> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(foundEntity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        if (casheEntryOptions is not null)
            await casheBroker.DeleteAsync<TEntity>(id.ToString());

        return foundEntity;
    }
}