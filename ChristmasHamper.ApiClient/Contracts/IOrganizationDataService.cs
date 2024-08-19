using ChristmasHamper.ApiClient.ViewModels;

namespace ChristmasHamper.ApiClient.Contracts;

public interface IOrganizationDataService
{
    Task<List<OrganizationViewModel>> GetAllOrganizations();
    Task<OrganizationViewModel> GetOrganizationById(int id);
}
