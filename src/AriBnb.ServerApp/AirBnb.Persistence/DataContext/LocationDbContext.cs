using AirBnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Persistence.DataContext;

public class LocationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Location> Locations => Set<Location>();

    public DbSet<LocationCategories> LocationCategories => Set<LocationCategories>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}