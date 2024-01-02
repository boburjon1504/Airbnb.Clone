using System.Linq.Expressions;
using AirBnb.Domain.Common.Cashing;
using AirBnb.Domain.Entities;
using AirBnb.Persistence.Cashing.Brokers;
using AirBnb.Persistence.DataContext;
using AirBnb.Persistence.Repository.Interfaces;

namespace AirBnb.Persistence.Repository;

public class LocationCategoriesRepository(LocationDbContext context, ICasheBroker broker) 
    : EntityRepositoryBase<LocationCategories, LocationDbContext>(context, broker, new CasheEntryOptions()),
        ILocationCategoriesRepository
{
    public ValueTask<IEnumerable<LocationCategories>> GetAllAsync(bool asNoTracking = false) =>
        new (base.Get(asNoTracking: true).ToList());

    public new IQueryable<LocationCategories> Get(Expression<Func<LocationCategories, bool>>? predicate = default, bool asNoTracking = false) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<LocationCategories?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<IList<LocationCategories>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<LocationCategories> CreateAsync(LocationCategories locationCategories, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.CreateAsync(locationCategories, saveChanges, cancellationToken);

    public new ValueTask<LocationCategories> UpdateAsync(LocationCategories locationCategories, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.UpdateAsync(locationCategories, saveChanges, cancellationToken);

    public new ValueTask<LocationCategories> DeleteAsync(LocationCategories locationCategories, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteAsync(locationCategories, saveChanges, cancellationToken);

    public new ValueTask<LocationCategories> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}