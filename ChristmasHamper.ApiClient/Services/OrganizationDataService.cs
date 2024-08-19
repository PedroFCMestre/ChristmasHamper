using AutoMapper;
using ChristmasHamper.ApiClient.Contracts;
using ChristmasHamper.ApiClient.Services.Base;
using ChristmasHamper.ApiClient.ViewModels;

namespace ChristmasHamper.ApiClient.Services;

public class OrganizationDataService : BaseDataService, IOrganizationDataService
{
    private readonly IMapper _mapper;

    public OrganizationDataService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<List<OrganizationViewModel>> GetAllOrganizations()
    {
        var organizations = await _client.GetAllOrganizationsAsync();
        var organizationsVm = _mapper.Map<ICollection<OrganizationViewModel>>(organizations);
        return organizationsVm.ToList();

    }

    public async Task<OrganizationViewModel> GetOrganizationById(int id)
    {
        var organization = await _client.GetOrganizationByIdAsync(id);
        var organizationVm = _mapper.Map<OrganizationViewModel>(organization);
        return organizationVm;
    }
}
