using ChristmasHamper.Domain.Entities;
using ChristmasHamper.Persistence.DbContexts;
using ChristmasHamper.Persistence.IntegrationTests.Base;
using FluentAssertions;

namespace ChristmasHamper.Persistence.IntegrationTests;

public class ChristmasHamperDbContextTests
{
    private readonly ChristmasHamperDbContext _dbContext;

    public ChristmasHamperDbContextTests()
    {
        _dbContext = ChristmasHamperContextFactory.Create();
    }

    [Fact]
    public async void Save_SetCreatedByProperty()
    {
        var organization = new Organization() { Name = "New Organization3", Acronym = "Og3" };

        _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        organization.CreatedBy.Should().NotBeNull();
        organization.CreatedBy.Should().Be("user1");
        organization.CreatedOn.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(3));
    }

    [Fact]
    public async void Save_SetLastModifiedByProperty()
    {
        var organization = _dbContext.Organizations.Find(1);

        organization!.Name = "Updated Organization1";
        _dbContext.Organizations.Update(organization);
        await _dbContext.SaveChangesAsync();

        organization.LastModifiedBy.Should().NotBeNull();
        organization.LastModifiedBy.Should().Be("user1");
        organization.LastModifiedOn.Should().NotBeNull();
        organization.LastModifiedOn.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(3));
    }
}
