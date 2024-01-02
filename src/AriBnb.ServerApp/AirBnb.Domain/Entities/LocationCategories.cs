using AirBnb.Domain.Common.Entities;

namespace AirBnb.Domain.Entities;

public class LocationCategories : Entity
{
    public string Name { get; set; } = default!;
    
    public string ImageUrl { get; set; } = default!;
}