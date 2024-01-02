using System.Linq.Expressions;
using AirBnb.Application.Services;
using AirBnb.Domain.Entities;
using AirBnb.Domain.Extensions;
using AirBnb.Domain.Settings;
using AirBnb.Persistence.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace AirBnb.Infrastructure.Services;

public class LocationCategoryService(ILocationCategoriesRepository locationCategoriesRepository, IOptions<ApiConfigurations> options) : ILocationCategoriesService
{
    public async ValueTask<IEnumerable<LocationCategories>> GetAllAsync(bool asNoTracking = false)
    {
        var result = (await locationCategoriesRepository
            .GetAllAsync(asNoTracking)).ToList();
        result.ForEach(category =>
        {
            category.ImageUrl = category.ImageUrl.ToUrl(options.Value.ApiUrl);
        });

        return result;
    }

    public IEnumerable<LocationCategories> Get(Expression<Func<LocationCategories, bool>>? predicate = default,
        bool asNoTracking = false)
    {
        var result = locationCategoriesRepository.Get(predicate, asNoTracking).ToList();
        
        result.ForEach(category =>
        {
            category.ImageUrl = category.ImageUrl.ToUrl(options.Value.ApiUrl);
        });

        return result;
    }

    public async ValueTask<LocationCategories?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var result = await locationCategoriesRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
        
        if (result is not null)
            result.ImageUrl = result.ImageUrl.ToUrl(options.Value.ApiUrl);

        return result;
    }

    public async ValueTask<IList<LocationCategories>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var result = (await locationCategoriesRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken)).ToList();
        
        result.ForEach(category =>
        {
            category.ImageUrl = category.ImageUrl.ToUrl(options.Value.ApiUrl);
        });

        return result;
    }

    public ValueTask<LocationCategories> CreateAsync(LocationCategories location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        location.Id = Guid.NewGuid();
    
        return locationCategoriesRepository.CreateAsync(location, saveChanges, cancellationToken);
    }

    public ValueTask<LocationCategories> UpdateAsync(LocationCategories location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationCategoriesRepository.UpdateAsync(location, saveChanges, cancellationToken);

    public ValueTask<LocationCategories> DeleteAsync(LocationCategories location, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationCategoriesRepository.DeleteAsync(location, saveChanges, cancellationToken);

    public ValueTask<LocationCategories> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        locationCategoriesRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}