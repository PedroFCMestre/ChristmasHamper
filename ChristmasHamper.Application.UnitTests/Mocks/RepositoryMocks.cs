using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace ChristmasHamper.Application.UnitTests.Mocks;

public class RepositoryMocks
{
    public static Mock<IOrganizationRepository> GetOrganizationRepository()
    {
        var organizations = new List<Organization>
        {
            new() {Id = 1, Name = "Organization1", Acronym = "Og1"},
            new() {Id = 2, Name = "Organization2", Acronym = "Og2"},
            new() {Id = 3, Name = "Organization3", Acronym = "Og3"}
        };

        var mockOrganizationRepository = new Mock<IOrganizationRepository>();

        
        mockOrganizationRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync((int id) =>
                                  {
                                      return organizations.Find(o => o.Id == id);
                                  });

        mockOrganizationRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(organizations);

        mockOrganizationRepository.Setup(repo => repo.AddAsync(It.IsAny<Organization>()))
                                  .ReturnsAsync((Organization organization) => 
                                                { 
                                                    organizations.Add(organization);
                                                    return organization;
                                                });

        mockOrganizationRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Organization>()))
                                  .ReturnsAsync((Organization organization) =>
                                  {
                                      var organizationToUpdate = organizations.FirstOrDefault(o => o.Id == organization.Id);
                                      organizationToUpdate!.Name = organization.Name;
                                      organizationToUpdate.Acronym = organization.Acronym;
                                      return 1;
                                  });

        mockOrganizationRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Organization>()))
                                  .ReturnsAsync((Organization organization) =>
                                  {
                                      organizations.Remove(organization);
                                      return 1;
                                  });


        mockOrganizationRepository.Setup(repo => repo.ExistsByIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync((int id) =>
                                  {
                                      return organizations.Any(o => o.Id == id);
                                  });

        mockOrganizationRepository.Setup(repo => repo.IsNameUnique(It.IsAny<string>()))
                                  .ReturnsAsync((string name) =>
                                  {
                                      return !organizations.Any(o => o.Name == name);
                                  });

        mockOrganizationRepository.Setup(repo => repo.IsNameUnique(It.IsAny<int>(), It.IsAny<string>()))
                                  .ReturnsAsync((int id, string name) =>
                                  {
                                      return !organizations.Any(o => o.Id != id && o.Name == name);
                                  });

        mockOrganizationRepository.Setup(repo => repo.IsAcronymUnique(It.IsAny<string>()))
                                  .ReturnsAsync((string acronym) =>
                                  {
                                      return !organizations.Any(o => o.Acronym == acronym);
                                  });

        mockOrganizationRepository.Setup(repo => repo.IsAcronymUnique(It.IsAny<int>(), It.IsAny<string>()))
                                  .ReturnsAsync((int id, string acronym) =>
                                  {
                                      return !organizations.Any(o => o.Id != id && o.Acronym == acronym);
                                  });

        return mockOrganizationRepository;
    }
}

