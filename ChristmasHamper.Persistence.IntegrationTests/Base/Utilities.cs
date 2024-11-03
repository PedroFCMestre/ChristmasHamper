using ChristmasHamper.Application.Contracts;
using ChristmasHamper.Domain.Entities;
using ChristmasHamper.Persistence.DbContexts;
using Moq;

namespace ChristmasHamper.Persistence.IntegrationTests.Base;

public static class Utilities
{
    public static Mock<ILoggedInUserService> MockLoggedInUserServiceForTests()
    {
        var loggedInUserId = "user1";
        var loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        loggedInUserServiceMock.Setup(m => m.UserId).Returns(loggedInUserId);

        return loggedInUserServiceMock;
    }

    public async static void InitializeDbForTests(ChristmasHamperDbContext dbContext)
    {
        dbContext.Organizations.Add(new Organization { Name = "Organization1", Acronym = "Og1" });
        dbContext.Organizations.Add(new Organization { Name = "Organization2", Acronym = "Og2" });

        await dbContext.SaveChangesAsync();
    }

    
}
