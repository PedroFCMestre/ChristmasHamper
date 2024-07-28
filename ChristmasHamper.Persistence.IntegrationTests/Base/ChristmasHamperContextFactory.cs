using ChristmasHamper.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ChristmasHamper.Persistence.IntegrationTests.Base;

public static class ChristmasHamperContextFactory
{
    public static ChristmasHamperDbContext Create()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ChristmasHamperDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var loggedInUserServiceMock = Utilities.MockLoggedInUserServiceForTests();

        var dbContext = new ChristmasHamperDbContext(dbContextOptions, loggedInUserServiceMock.Object);

        try
        {
            Utilities.InitializeDbForTests(dbContext);
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred seeding the database with test messages. Error: {ex.Message}");
        }

        return dbContext;
    }
}
