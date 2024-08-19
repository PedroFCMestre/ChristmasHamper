using AutoMapper;
using ChristmasHamper.ApiClient.Services.Base;
using ChristmasHamper.ApiClient.ViewModels;

namespace ChristmasHamper.ApiClient.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetOrganizationsListQueryResponse, OrganizationViewModel>();
    }
}
