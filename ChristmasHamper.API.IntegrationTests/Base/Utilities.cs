using ChristmasHamper.Persistence.DbContexts;
using System.Text.Json;

namespace ChristmasHamper.API.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTests(ChristmasHamperDbContext context)
    {
        context.Organizations.Add(new Domain.Entities.Organization { Id = 1, Name = "Organization1", Acronym = "Og1" });
        context.Organizations.Add(new Domain.Entities.Organization { Id = 2, Name = "Organization2", Acronym = "Og2" });
        context.Organizations.Add(new Domain.Entities.Organization { Id = 3, Name = "Organization3", Acronym = "Og3" });

        context.SaveChanges();
    }

    public static readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
