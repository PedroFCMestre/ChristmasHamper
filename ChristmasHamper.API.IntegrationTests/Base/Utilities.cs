using ChristmasHamper.Domain.Entities;
using ChristmasHamper.Persistence.DbContexts;
using System.Text.Json;

namespace ChristmasHamper.API.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTests(ChristmasHamperDbContext dbContext)
    {
        dbContext.AddRange(
            new List<Organization>
            {
                new() { Name = "Organization1", Acronym = "Og1" },
                new() { Name = "Organization2", Acronym = "Og2" },
                new() { Name = "Organization3", Acronym = "Og3" }
            });

        dbContext.SaveChanges();
    }

    public static readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
