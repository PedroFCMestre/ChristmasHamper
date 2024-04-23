using ChristmasHamper.Application.Contracts;
using ChristmasHamper.Domain.Entities;
using ChristmasHamper.Persistence.DbContexts;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChristmasHamper.Persistence.IntegrationTests;

public class ChristmasHamperDbContextTests
{
    private readonly ChristmasHamperDbContext _dbContext;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;

    public ChristmasHamperDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ChristmasHamperDbContext>().UseInMemoryDatabase("ChristmasHamperDbContextInMemoryTest").Options;

        _loggedInUserId = "user1";
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

        _dbContext = new ChristmasHamperDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    [Fact]
    public async void Save_SetCreatedByProperty()
    {
        var organization = new Organization() { Id = 1, Name = "New Organization", Acronym = "NewOg" };

        _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        organization.CreatedBy.Should().Be("user1");
    }

    [Fact]
    public async void Save_SetLastModifiedByProperty()
    {
        var organization = new Organization() { Id = 2, Name = "New Organization", Acronym = "NewOg" };
        _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        organization.Name = "Update Organization";
        _dbContext.Organizations.Update(organization);
        await _dbContext.SaveChangesAsync();

        organization.LastModifiedBy.Should().Be("user1");
    }
}
