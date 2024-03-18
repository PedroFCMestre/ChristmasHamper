using ChristmasHamper.Domain.Entities;

namespace ChristmasHamper.Application.Contracts.Persistence;

public interface IOrganizationRepository : IAsyncRepository<Organization>
{
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> IsNameUnique(string name);
    Task<bool> IsNameUnique(int id, string name);
    Task<bool> IsAcronymUnique(string acronym);
    Task<bool> IsAcronymUnique(int id, string acronym);
}

