using ChristmasHamper.ApiClient.Contracts;
using ChristmasHamper.ApiClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasHamper.ApiClient;

public static class ApiClientServiceRegistration
{
    public static IServiceCollection AddApiClientServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IOrganizationDataService, OrganizationDataService>();

        return services;
    }
}
