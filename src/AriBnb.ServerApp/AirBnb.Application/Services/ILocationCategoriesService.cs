using System.Linq.Expressions;
using AirBnb.Domain.Entities;

namespace AirBnb.Application.Services;

public interface ILocationCategoriesService
{
    ValueTask<IEnumerable<LocationCategories>> GetAllAsync(bool asNoTracking = false);

    IEnumerable<LocationCategories> Get(Expression<Func<LocationCategories, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<LocationCategories?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<IList<LocationCategories>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<LocationCategories> CreateAsync(LocationCategories locationCategories, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<LocationCategories> UpdateAsync(LocationCategories locationCategories, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<LocationCategories> DeleteAsync(LocationCategories locationCategories, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<LocationCategories> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}