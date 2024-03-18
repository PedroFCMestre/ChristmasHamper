using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using ChristmasHamper.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace ChristmasHamper.Persistence.Repositories;

public class OrganizationRepository(ChristmasHamperDbContext dbContext) : BaseRepository<Organization>(dbContext), IOrganizationRepository
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbContext.Organizations.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> IsNameUnique(string name)
    {
        return !await _dbContext.Organizations.AnyAsync(e => e.Name == name);
    }

    public async Task<bool> IsNameUnique(int id, string name)
    {
        return !await _dbContext.Organizations.AnyAsync(e => e.Id != id && e.Name == name);
    }
    public async Task<bool> IsAcronymUnique(string acronym)
    {
        return !await _dbContext.Organizations.AnyAsync(e => e.Acronym == acronym);
    }

    public async Task<bool> IsAcronymUnique(int id, string acronym)
    {
        return !await _dbContext.Organizations.AnyAsync(e => e.Id != id && e.Acronym == acronym);
    }    
}

