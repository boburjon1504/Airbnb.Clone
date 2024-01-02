using System.Linq.Expressions;
using AirBnb.Application.Services;
using AirBnb.Domain.Entities;
using AirBnb.Persistence.Repository.Interfaces;

namespace AirBnb.Infrastructure.Services;

public class LocationService(ILocationRepository locationRepository) : ILocationService
{
    public ValueTask<IEnumerable<Location>> GetAllAsync(bool asNoTracking = false) =>
        locationRepository.GetAllAsync(asNoTracking);

    public IEnumerable<Location> Get(Expression<Func<Location, bool>>? predicate = default, bool asNoTracking = false) =>
        locationRepository.Get(predicate, asNoTracking);

    public ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        locationRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<Location>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        locationRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<Location> CreateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        location.Id = Guid.NewGuid();
    
        return locationRepository.CreateAsync(location, saveChanges, cancellationToken);
    }

    public ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationRepository.UpdateAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> DeleteAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationRepository.DeleteAsync(location, saveChanges, cancellationToken);

    public ValueTask<Location> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}