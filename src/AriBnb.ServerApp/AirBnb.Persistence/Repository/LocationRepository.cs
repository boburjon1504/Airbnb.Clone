using System.Linq.Expressions;
using AirBnb.Domain.Common.Cashing;
using AirBnb.Domain.Entities;
using AirBnb.Persistence.Cashing.Brokers;
using AirBnb.Persistence.DataContext;
using AirBnb.Persistence.Repository.Interfaces;

namespace AirBnb.Persistence.Repository;

public class LocationRepository(LocationDbContext context, ICasheBroker broker) 
    : EntityRepositoryBase<Location, LocationDbContext>(context, broker, new CasheEntryOptions()),
        ILocationRepository
{
    public ValueTask<IEnumerable<Location>> GetAllAsync(bool asNoTracking = false) =>
        new (base.Get(asNoTracking: true).ToList());

    public new IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, bool asNoTracking = false) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<IList<Location>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<Location> CreateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.CreateAsync(location, saveChanges, cancellationToken);

    public new ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.UpdateAsync(location, saveChanges, cancellationToken);

    public new ValueTask<Location> DeleteAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteAsync(location, saveChanges, cancellationToken);

    public new ValueTask<Location> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}