﻿using ChristmasHamper.Domain.Entities;

namespace ChristmasHamper.Application.Contracts.Persistence;

public interface IOrganizationRepository : IAsyncRepository<Organization>
{
    Task<bool> IsNameUnique(string name);
    Task<bool> IsNameUnique(int organizationsId, string name);
    Task<bool> IsAcronymUnique(string acronym);
    Task<bool> IsAcronymUnique(int organizationsId, string acronym);
}

