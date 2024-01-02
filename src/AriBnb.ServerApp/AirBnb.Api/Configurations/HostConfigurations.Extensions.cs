using System.Reflection;
using AirBnb.Application.Services;
using AirBnb.Domain.Settings;
using AirBnb.Infrastructure.Common.Cashing.Brokers;
using AirBnb.Infrastructure.Common.Settings;
using AirBnb.Infrastructure.Services;
using AirBnb.Persistence.Cashing.Brokers;
using AirBnb.Persistence.DataContext;
using AirBnb.Persistence.Repository;
using AirBnb.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Api.Configurations;

public static partial class HostConfigurations
{
    private static IList<Assembly> Assemblies;

    static HostConfigurations()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<ILocationCategoriesService, LocationCategoryService>();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<LocationDbContext>(option => 
                option.UseNpgsql(builder.Configuration.GetConnectionString("LocationDbContext")));

        builder.Services
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<ILocationCategoriesRepository, LocationCategoriesRepository>();

        return builder;
    }

    private static WebApplicationBuilder AddCashing(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CasheSettings>(builder.Configuration.GetSection(nameof(CasheSettings)));

        builder.Services.AddLazyCache();
        builder.Services.AddSingleton<ICasheBroker, LazyMemoryCasheBroker>();
        
        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddFront(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<ApiConfigurations>(builder.Configuration.GetSection("ApiConfigurations"));
        
        builder.Services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowAnyHeader();
                    });
            });
        
        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(route => route.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseStaticFiles();

        app.UseCors();
        
        app.MapControllers();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}