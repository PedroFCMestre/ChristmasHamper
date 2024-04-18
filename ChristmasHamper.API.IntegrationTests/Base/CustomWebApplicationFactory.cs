using ChristmasHamper.Persistence.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace ChristmasHamper.API.IntegrationTests.Base;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ChristmasHamperDbContext>));

            services.AddDbContext<ChristmasHamperDbContext>(options =>
            {
                options.UseInMemoryDatabase("ChristmasHamperDbContextInMemoryTest");
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ChristmasHamperDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

                context.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTests(context);
                }
                catch (Exception ex) 
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            }
        });
    }

}
