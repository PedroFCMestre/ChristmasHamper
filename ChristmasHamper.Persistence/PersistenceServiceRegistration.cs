using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Persistence.DbContexts;
using ChristmasHamper.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasHamper.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChristmasHamperDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ChristmasHamperConnectionString")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        return services;
    }
}

