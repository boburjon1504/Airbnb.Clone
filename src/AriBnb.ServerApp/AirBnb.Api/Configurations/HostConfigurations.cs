namespace AirBnb.Api.Configurations;

public static partial class HostConfigurations
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddInfrastructure()
            .AddPersistence()
            .AddCashing()
            .AddMappers()
            .AddFront()
            .AddExposers()
            .AddDevTools();
        return new (builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseExposers()
            .UseDevTools();
    
        return new(app);
    }
}